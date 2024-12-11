using DAL.DbRHDCV2Context;
using DAL.Entities.MappingTables;
using DAL.Enums;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;
using Shared.Managers.Interfaces;
using Shared.Models;
using Shared.Models.ScrapingModels;
using System.Net;
using System.Text.RegularExpressions;

namespace Shared.Managers
{
    public class WebScrapingManager : IWebScrapingManager
    {
        private readonly IConfiguration _configuration;
        private readonly RHDCV2Context _context;
        private readonly IErrorLogManager _error;

        public WebScrapingManager(IConfiguration configuration, RHDCV2Context context, IErrorLogManager error)
        {
            _configuration = configuration;
            _context = context;
            _error = error;
        }

        #region PUBLIC
        public URLGenerationModel GenerateNextResultRetrievalURL()
        {
            var result = new URLGenerationModel();

            try
            {
                //Get most recent date retrieved
                var history = _context.tb_auto_retriever_log;
                //If there are no existing runs, use yesterdays date
                if (history == null || history.Count() == 0)
                {
                    var yesterdaysDate = DateTime.Now.Date.AddDays(-1);
                    result.Url = BuildResultUrl(yesterdaysDate);
                    result.EventDate = yesterdaysDate;

                    return result;
                }

                var failedEvent = history.FirstOrDefault(x => !x.Success && x.Retries < 3);

                if (failedEvent != null) 
                {
                    result.EventDate = failedEvent.DateRetrieved.Date;
                    result.Url = BuildResultUrl(failedEvent.DateRetrieved.Date);
                }

                var dateToUse = GetMissingDate(history.Select(x => x.DateRetrieved).ToList());
                result.EventDate = dateToUse;
                result.Url = BuildResultUrl(dateToUse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
        public async Task<HtmlDocument> Scrape(string url)
        {
            try
            {
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);
                await Task.Delay(5000); // This'll slow things down but at least it doesnt look like I'm trying to DDOS anyone
                return htmlDoc;
            }
            catch (Exception ex)
            {
                await _error.LogError("", "WebScrapingManager.cs", "Scrape", ErrorType.HttpRequest, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                throw new Exception(ex.Message);
            }
        }

        public async Task AddAutoretrieverLog(DateTime date, bool success, string note, int retries = 0)
        {
            try 
            {
                _context.tb_auto_retriever_log.Add(new DAL.Entities.AutoRetrieverLog()
                {
                    DateRetrieved = date,
                    Success = success,
                    Note = note,
                    Retries = retries
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _error.LogError("tb_auto_retriever_log", "WebScrapingManager.cs", "AddAutoretrieverLog", ErrorType.Database, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ScrapingEventModel>> ScrapeAllData(HtmlDocument htmlDoc, DateTime raceDate) 
        {
            var eventData = ScrapeEventData(htmlDoc, raceDate);
            
            foreach (var e in eventData.Where(x => !String.IsNullOrEmpty(x.CourseName)))
            {
                var raceData = await ScrapeRaceData(htmlDoc, e);
                e.Races = raceData;
            
                foreach (var race in raceData)
                {
                    var raceHorseData = await ScrapeRaceHorseData(race);
                    if (raceHorseData.Count() == 0) 
                    {
                        race.Abandoned = true;
                    }
                    race.RaceHorses = raceHorseData;
                }
            
            }

            return eventData;
        }

        private List<ScrapingEventModel> ScrapeEventData(HtmlDocument htmlDoc, DateTime raceDate)
        {
            var result = new List<ScrapingEventModel>();
            var courses = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'panel push--x-small')]");
            foreach (var course in courses) 
            {
                var e = new ScrapingEventModel();
                e.EventDate = raceDate;

                //Get course name and validate its a course we want to use
                var courseNameHtml = course.SelectSingleNode(".//h2[contains(@class, 'h6')]")?.InnerHtml ?? "";
                var courseName = ValidateCourseName(courseNameHtml);

                if (String.IsNullOrEmpty(courseName))
                    continue;

                e.CourseName = courseName;

                result.Add(e);
            }
            return result;
        }

        private async Task<List<ScrapingRaceModel>> ScrapeRaceData(HtmlDocument htmlDoc, ScrapingEventModel e)
        {
            var result = new List<ScrapingRaceModel>();

            try 
            {
                //Using Regex to filter out brackets, and anything within the brackets
                var courseNameForURL = Regex.Replace(e.CourseName, @"\s?\(.*?\)", "");
                var raceContainer = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@id,'todays-results-" + courseNameForURL.Replace(" ", "-").Replace("Bangor-On-Dee", "Bangor-on-Dee") + "')]"); // Ridiculous quick fix
                var raceData = raceContainer.SelectNodes(".//article");

                foreach (var race in raceData)
                {
                    var toAdd = new ScrapingRaceModel();
                    //Pick out the race Url and scrape the new page
                    var extension = race.SelectSingleNode(".//a[contains(@class,'post-text__t')]").Attributes["href"].Value;
                    var raceUrl = BaseUrlWithExtension(extension);
                    var raceHtml = await Scrape(raceUrl);

                    //Now we have race data, we need to pick out the attributes
                    toAdd.RaceUrl = raceUrl;

                    //Race Type -> Check the race name for .Contains racetype. Unfortunately this is the only way we can do this...
                    var raceHeader = raceHtml.DocumentNode.SelectSingleNode("//div[contains(@class,'race-header__details--primary')]");
                    var raceNameDiv = raceHeader.SelectSingleNode(".//p[contains(@class,'p--medium')]");
                    var raceName = raceNameDiv.SelectSingleNode(".//b").InnerHtml.ToLower();
                    toAdd.RaceType = raceName.Contains("hurdle") ? RaceType.Hurdles :
                        raceName.Contains("chase") ? RaceType.Chase :
                        raceName.Contains("handicap") ? RaceType.Handicap :
                        RaceType.Flat;
                    var classAndAges = raceHeader.SelectSingleNode(".//p[not(@class)]").InnerHtml;
                    var classAndAgesSplit = classAndAges.Split("|");
                    var c = "";
                    var a = "";

                    //Irish Races don't have classes
                    if (classAndAgesSplit.Count() < 2)
                    {
                        c = "0";
                        a = classAndAgesSplit[0].Trim();
                    }
                    //Cut out division
                    else if (classAndAgesSplit.Count() > 2)
                    {
                        c = classAndAgesSplit[1].Trim().ToLower().Replace("class ", "");
                        a = classAndAgesSplit[2].Trim();
                    }
                    else
                    {
                        c = classAndAgesSplit[0].Trim().ToLower().Replace("class ", "");
                        a = classAndAgesSplit[1].Trim();
                    }

                    toAdd.Class = c;
                    toAdd.AgeCategoryName = a;

                    var raceDetailsDiv = raceHtml.DocumentNode.SelectSingleNode("//div[contains(@class,'race-header__details--secondary')]");
                    var going = raceDetailsDiv.SelectSingleNode(".//p[contains(@class,'p--medium')]").InnerHtml.ToLower();
                    var distance = raceDetailsDiv.SelectSingleNode(".//div[contains(@class,'p--large')]").InnerHtml.ToLower();

                    toAdd.GoingCategoryName = going.Trim();
                    toAdd.DistanceCategoryName = distance.Trim();

                    result.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                await _error.LogError("", "WebScrapingManager.cs", "ScrapeRaceData", ErrorType.WebScraping, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                throw new Exception(ex.Message);
            }


            return result;
        }

        private async Task<List<ScrapingRaceHorseModel>> ScrapeRaceHorseData(ScrapingRaceModel race)
        {
            var result = new List<ScrapingRaceHorseModel>();


            try 
            {
                if (String.IsNullOrEmpty(race.RaceUrl))
                    throw new Exception("Failed to scrape race. No URL Provided");

                var racePage = await Scrape(race.RaceUrl!);
                var raceContainer = racePage.DocumentNode.SelectSingleNode("//div[contains(@class,'card__content')]");
                if (raceContainer == null) 
                {
                    return result;
                }
                var raceHorseContainers = raceContainer.SelectNodes(".//div[contains(@class, 'card-entry') and not(contains(@class, 'card-entry--non-runner'))]");

                foreach (var container in raceHorseContainers)
                {
                    var raceHorse = new ScrapingRaceHorseModel();
                    var position = HTMLAgilityPackHelpers.GetTextOnlyFromDiv(container.SelectSingleNode(".//span[contains(@class,'p--large')]"));
                    var horseName = HTMLAgilityPackHelpers.GetTextOnlyFromDiv(container.SelectSingleNode(".//a[contains(@class,'horse__link')]"));
                    var odds = HTMLAgilityPackHelpers.GetTextOnlyFromDiv(container.SelectSingleNode(".//div[contains(@class,'card-cell--odds')]"));
                    var ageAndWeight = container.SelectSingleNode(".//div[contains(@class,'card-cell--stats')]");
                    var ageAndWeightSplit = ageAndWeight.InnerText.Trim().Split("\r\n");
                    var age = ageAndWeightSplit[0];
                    var weight = ageAndWeightSplit[1];
                    var attire = ageAndWeight.SelectSingleNode(".//span")?.InnerText?.Trim() ?? "";
                    var trainerAndJockey = container.SelectNodes(".//span[contains(@class,'icon-text__t')]");
                    var jockey = "";
                    var trainer = "";
                    if (trainerAndJockey.Count() >= 2) 
                    {
                         jockey = HTMLAgilityPackHelpers.GetTextOnlyFromDiv(trainerAndJockey[0]);
                         trainer = HTMLAgilityPackHelpers.GetTextOnlyFromDiv(trainerAndJockey[1]);
                    }

                    var distanceBetween = HTMLAgilityPackHelpers.GetTextOnlyFromDiv(container.SelectSingleNode(".//div[contains(@class,'card-cell--form')]"));

                    if (!String.IsNullOrEmpty(distanceBetween))
                    {
                        string decoded = WebUtility.HtmlDecode(distanceBetween);
                        distanceBetween = StringHelper.ReplaceFractionsWithDecimals(decoded);
                    }

                    raceHorse.Position = position.Trim();
                    raceHorse.HorseName = StringHelper.FormatName(horseName.Trim());
                    raceHorse.Odds = StringHelper.FormatName(odds.Replace("J", "").ToLower()).Trim();
                    raceHorse.Age = age.Trim();
                    raceHorse.Weight = weight.Trim();
                    raceHorse.JockeyName = StringHelper.FormatName(jockey.Trim());
                    raceHorse.TrainerName = StringHelper.FormatName(trainer.Trim());
                    raceHorse.DistanceBetween = distanceBetween.Trim();
                    raceHorse.Attire = attire;
                    result.Add(raceHorse);
                }
            }
            catch (Exception ex)
            {
                await _error.LogError("", "WebScrapingManager.cs", "ScrapeRaceHorseData", ErrorType.WebScraping, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                throw new Exception(ex.Message);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// Identify the next missing date I need to scrape data for, and return it to be used for the URL Generator
        /// </summary>
        /// <param name="dateList">List of existing dates I have data for</param>
        /// <returns></returns>
        #region PRIVATE
        private DateTime GetMissingDate(List<DateTime> dateList)
        {
            dateList = dateList.OrderByDescending(d => d).ToList();

            DateTime today = DateTime.Now.Date;
            // Start checking from yesterday (not going to scrape todays races here)
            DateTime currentDate = today.AddDays(-1);

            while (currentDate >= dateList.Min().AddDays(-1))
            {
                // If yesterday is not in the list, return it
                if (!dateList.Contains(currentDate))
                {
                    return currentDate;
                }

                // else, skip
                currentDate = currentDate.AddDays(-1);
            }

            return DateTime.MinValue; 
        }

        /// <summary>
        /// Build the complete AtTheRaces URL Based on the passed in Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string BuildResultUrl(DateTime date)
        {
            var dateString = date.ToString("dd-MMMM-yyyy");
            var baseUrl = _configuration["ExternalURLs:AtTheRacesBaseUrl"] + "/results/";
            return $"{baseUrl}{dateString}";
        }

        /// <summary>
        /// Used to fetch the base url and combine it with the scraped race URL
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string BaseUrlWithExtension(string url)
        {
            return _configuration["ExternalURLs:AtTheRacesBaseUrl"] + url;
        }

        /// <summary>
        /// Get Raw HTML Data of passed in URL
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<string> CallUrl(string fullUrl)
        {
            try
            {
                //Get the HTML from a specific page
                var handler = new HttpClientHandler()
                {
                    AllowAutoRedirect = false
                };

                HttpClient client = new HttpClient(handler);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetStringAsync(fullUrl);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Validate which courses we want to use, for now, stick to UK and Ireland < - Change to table in DB
        /// </summary>
        /// <param name="html"> Inner HTML Of the Event Title Divs h2 element</param>
        /// <returns></returns>
        private string ValidateCourseName(string html) 
        {
            var result = "";

            if (String.IsNullOrEmpty(html))
                return result;

            if (html.Contains("Abandoned"))
                return result;

            if (html.Contains("France"))
                return result;

            if (html.Contains("USA"))
                return result;

            if (html.Contains("RSA"))
                return result;

            if (html.Contains("Hong"))
                return result;

            if (html.Contains("Brazil"))
                return result;

            if (html.Contains("Argentina"))
                return result;

            if (html.Contains("Germany"))
                return result;

            if (html.Contains("Czech"))
                return result;

            if (html.Contains("Chile"))
                return result;

            if (html.Contains("Japan"))
                return result;

            if (html.Contains("IRE PTP"))
                return result;

            if (html.Contains("Dubai"))
                return result;

            if (html.Contains("Australia"))
                return result;

            if (html.Contains("Bahrain"))
                return result;

            result = html.Replace("Results", "").Trim();

            return result;
        }
        #endregion

    }
}
