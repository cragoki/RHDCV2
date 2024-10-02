using DAL.DbRHDCV2Context;
using DAL.Enums;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Shared.Managers.Interfaces;
using Shared.Models;
using Shared.Models.ScrapingModels;
using System.Net;

namespace Shared.Managers
{
    public class WebScrapingManager : IWebScrapingManager
    {
        private readonly IConfiguration _configuration;
        private readonly RHDCV2Context _context;

        public WebScrapingManager(IConfiguration configuration, RHDCV2Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        #region PUBLIC
        public URLGenerationModel GenerateNextResultRetrievalURL()
        {
            var result = new URLGenerationModel();

            try
            {
                //Get most recent date retrieved
                var history = _context.tb_auto_retriever_log.Select(x => x.DateRetrieved).ToList();

                //If there are no existing runs, use yesterdays date
                if (history == null || history.Count() == 0)
                {
                    var yesterdaysDate = DateTime.Now.Date.AddDays(-1);
                    result.Url = BuildResultUrl(yesterdaysDate);
                    result.EventDate = yesterdaysDate;

                    return result;
                }

                var dateToUse = GetMissingDate(history);
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
            var page = await CallUrl(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(page);
            await Task.Delay(5000); // This'll slow things down but at least it doesnt look like I'm trying to DDOS anyone
            return htmlDoc;
        }

        public async Task AddAutoretrieverLog(DateTime date, bool success)
        {
            _context.tb_auto_retriever_log.Add(new DAL.Entities.AutoRetrieverLog()
            {
                DateRetrieved = date,
                Success = success
            });

            await _context.SaveChangesAsync();
        }

        public async Task ScrapeAllData(HtmlDocument htmlDoc, DateTime raceDate) 
        {
            var eventData = ScrapeEventData(htmlDoc, raceDate);
            
            foreach (var e in eventData.Where(x => !String.IsNullOrEmpty(x.CourseName)))
            {
                var raceData = await ScrapeRaceData(htmlDoc, e);
                e.Races = raceData;
            
                foreach (var race in raceData)
                {
                    var raceHorseData = await ScrapeRaceHorseData(race);
                    race.RaceHorses = raceHorseData;
                }
            
            }
        }

        private List<EventModel> ScrapeEventData(HtmlDocument htmlDoc, DateTime raceDate)
        {
            var result = new List<EventModel>();
            var courses = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'panel push--x-small')]");
            foreach (var course in courses) 
            {
                var e = new EventModel();
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

        private async Task<List<RaceModel>> ScrapeRaceData(HtmlDocument htmlDoc, EventModel e)
        {
            var result = new List<RaceModel>();

            var raceContainer = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@id,'todays-results-" + e.CourseName + "')]");
            var raceData = raceContainer.SelectNodes(".//article");

            foreach (var race in raceData) 
            {
                var toAdd = new RaceModel();
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

                if (classAndAgesSplit.Count() < 2)
                {
                    throw new Exception("Failed to Scrape. Class and Age Group not formatted as expected");
                }
                toAdd.Class = classAndAgesSplit[0].Trim().ToLower().Replace("class ", "");
                toAdd.AgeCategoryName = classAndAgesSplit[1].Trim();

                //Get the Going
                var raceDetailsDiv = raceHtml.DocumentNode.SelectSingleNode("//div[contains(@class,'race-header__details--secondary')]");
                var going = raceDetailsDiv.SelectSingleNode(".//p[contains(@class,'p--medium')]").InnerHtml.ToLower();
                var distance = raceDetailsDiv.SelectSingleNode(".//div[contains(@class,'p--large')]").InnerHtml.ToLower();

                toAdd.GoingCategoryName = going.Trim();
                toAdd.DistanceCategoryName = distance.Trim();

                result.Add(toAdd);
            }

            return result;
        }

        private async Task<List<RaceHorseModel>> ScrapeRaceHorseData(RaceModel race)
        {
            var result = new List<RaceHorseModel>();

            if (String.IsNullOrEmpty(race.RaceUrl))
                throw new Exception("Failed to scrape race. No URL Provided");

            var racePage = await Scrape(race.RaceUrl!);
            var raceContainer = racePage.DocumentNode.SelectSingleNode("//div[contains(@class,'card__content')]");
            var raceHorseContainers = raceContainer.SelectNodes(".//div[contains(@class, 'card-entry')]");

            foreach (var container in raceHorseContainers) 
            {
                var raceHorse = new RaceHorseModel();
                var position = container.SelectSingleNode(".//span[contains(@class,'p--large')]").InnerHtml;
                var horseName = container.SelectSingleNode(".//a[contains(@class,'horse__link')]").InnerHtml;
                var odds = container.SelectSingleNode(".//div[contains(@class,'card-cell--odds')]").InnerHtml;
                var ageAndWeight = container.SelectSingleNode(".//div[contains(@class,'card-cell--stats')]").InnerHtml.Trim(); // THis is broken...
                var age = ageAndWeight.Split(" ")[0];
                var weight = ageAndWeight.Split(" ")[1];
                var trainerAndJockey = container.SelectNodes(".//span[contains(@class,'icon-text__t')]");
                var jockey = trainerAndJockey[0].InnerHtml;
                var trainer = trainerAndJockey[1].InnerHtml;
                var distanceBetween = container.SelectSingleNode(".//div[contains(@class,'card-cell--form')]").InnerHtml;
                var time = ""; // TODO, may even need to do a new scrape request

                raceHorse.Position = position.Trim();
                raceHorse.HorseName = horseName.Trim();
                raceHorse.Odds = odds.Trim();
                raceHorse.Age = age.Trim();
                raceHorse.Weight = weight.Trim();
                raceHorse.JockeyName = jockey.Trim();
                raceHorse.TrainerName = trainer.Trim();
                raceHorse.DistanceBetween = distanceBetween.Trim();
                raceHorse.Time = time;
                result.Add(raceHorse);
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
        /// Validate which courses we want to use, for now, stick to UK and Ireland
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

            result = html.Replace("Results", "").Trim();

            return result;
        }
        #endregion

    }
}
