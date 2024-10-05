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
            Console.WriteLine("Initializing RHDC AutoRetriever");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking For Updates...");

                var worker = _context.tb_worker_service.FirstOrDefault(x => x.Name == WorkerServiceConstants.AutoRetriever);

                if (worker == null) 
                {
                    //Log Error 
                    await _error.LogError("tb_worker_service", "AutoRetrieverWorker.cs", "ExecuteAsync", ErrorType.Setup, null!, null!, null!);
                    throw new Exception("Auto Retriever Worker not found in the database");
                }

                if (!worker.Enabled) 
                {
                    Console.WriteLine("Auto Retriever has been disabled");
                    return;
                }

                if (worker.Start)
                {
                    //Identify the last day we are missing race data for, and produce a scrape url for that day
                    var urlData = _webScrapingManager.GenerateNextResultRetrievalURL();
                    try
                    {
                        Console.WriteLine("Auto Retriever Started, checking for race data");

                        //Get the Events for the chosen day's base page
                        var htmlDoc = await _webScrapingManager.Scrape(urlData.Url!);
                        //Scrape Data
                        var scrapedData = await _webScrapingManager.ScrapeAllData(htmlDoc, urlData.EventDate);
                        //Adding Scraped Data to the database
                        await _dataManager.AddEventAndRaceData(scrapedData);


                        //Lastly, delete all resolved errors...
                        Console.WriteLine("Auto Retriever Complete");
                        await _webScrapingManager.AddAutoretrieverLog(urlData.EventDate, true, "Success");
                    }
                    catch(Exception ex)
                    {
                        await _webScrapingManager.AddAutoretrieverLog(urlData.EventDate, false, ex.Message);
                    }

                    worker.LastRun = DateTime.Now;
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                else 
                {
                    Console.WriteLine("Auto Retriever Sleeping...");
                }

                Thread.Sleep((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            }

        }

    }
}
