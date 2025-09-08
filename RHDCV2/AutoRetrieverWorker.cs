using Shared.Constants;
using Shared.Managers.Interfaces;
using DAL.DbRHDCV2Context;
using DAL.Enums;
using Microsoft.Extensions.Hosting;

namespace AutoRetriever
{
    public class AutoRetrieverWorker : BackgroundService
    {
        private readonly RHDCV2Context _context;
        private readonly IErrorLogManager _error;
        private readonly IWebScrapingManager _webScrapingManager;
        private readonly IDatabaseManager _dataManager;

        public AutoRetrieverWorker(RHDCV2Context context, IErrorLogManager error, IWebScrapingManager webScrapingManager, IDatabaseManager dataManager) 
        {
            _context = context;
            _error = error;
            _webScrapingManager = webScrapingManager;
            _dataManager = dataManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Initializing {WorkerServiceConstants.AutoRetriever}");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking For Updates...");

                var worker = _context.tb_worker_service.FirstOrDefault(x => x.Name == WorkerServiceConstants.AutoRetriever);

                if (worker == null) 
                {
                    //Log Error 
                    await _error.LogError("tb_worker_service", "AutoRetrieverWorker.cs", "ExecuteAsync", ErrorType.Setup, null!, null!, null!);
                    throw new Exception($"{WorkerServiceConstants.AutoRetriever} not found in the database");
                }

                if (!worker.Enabled) 
                {
                    Console.WriteLine($"{WorkerServiceConstants.AutoRetriever} has been disabled");
                    return;
                }

                var time = DateTime.Now;

                if (worker.Start)
                {
                    await _dataManager.ClearTodaysRaces();

                    //Identify the last day we are missing race data for, and produce a scrape url for that day
                    var urlData = _webScrapingManager.GenerateNextResultRetrievalURL();
                    var existingRetrieverLog = _context.tb_auto_retriever_log.FirstOrDefault(x => x.DateRetrieved.Date == urlData.EventDate.Date);

                    try
                    {

                        if (urlData.EventDate.Date < DateTime.Now.AddYears(-1)) 
                        {
                            Console.WriteLine("Auto Retriever is up to date");
                            //Set Start = false and trigger scrape todays races automator.
                            return;
                        }
                        Console.WriteLine($"{WorkerServiceConstants.AutoRetriever} Started, Scraping races for date {urlData.EventDate.ToString("dd-MM-yyy")}");

                        //Get the Events for the chosen day's base page
                        var htmlDoc = await _webScrapingManager.Scrape(urlData.Url!);

                        //Scrape Data
                        var scrapedData = await _webScrapingManager.ScrapeAllData(htmlDoc, urlData.EventDate);

                        Console.WriteLine($"Scraping successful, adding data to db...");

                        //Adding Scraped Data to the database
                        await _dataManager.AddEventAndRaceData(scrapedData, false);

                        Console.WriteLine($"Events Added.");


                        //Lastly, delete all resolved errors...
                        Console.WriteLine($"{WorkerServiceConstants.AutoRetriever} Complete");

                        if (existingRetrieverLog == null)
                        {
                            await _webScrapingManager.AddAutoretrieverLog(urlData.EventDate, true, "Success");
                        }
                        else 
                        {
                            existingRetrieverLog.Note = "Success";
                            existingRetrieverLog.Success = true;
                        }
                    }
                    catch(Exception ex)
                    {
                        if (existingRetrieverLog == null)
                        {
                            await _webScrapingManager.AddAutoretrieverLog(urlData.EventDate, false, ex.Message);
                        }
                        else 
                        {
                            existingRetrieverLog.Success = false;
                            existingRetrieverLog.Retries = existingRetrieverLog.Retries + 1;
                        }

                        Console.WriteLine($"{WorkerServiceConstants.AutoRetriever} Failed...");

                    }

                    worker.LastRun = DateTime.Now;
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                else 
                {
                    Console.WriteLine($"{WorkerServiceConstants.AutoRetriever} Sleeping...");
                }

                Thread.Sleep((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            }

        }

    }
}
