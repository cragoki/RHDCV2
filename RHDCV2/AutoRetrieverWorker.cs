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

        public AutoRetrieverWorker(RHDCV2Context context, IErrorLogManager error, IWebScrapingManager webScrapingManager) 
        {
            _context = context;
            _error = error;
            _webScrapingManager = webScrapingManager;
        }

        private void OnStopping()
        {
            //
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
                    var urlData = _webScrapingManager.GenerateNextResultRetrievalURL();
                    try
                    {
                        Console.WriteLine("Auto Retriever Started, checking for race data");
                        var htmlDoc = await _webScrapingManager.Scrape(urlData.Url!);
                        var scrapedData = await _webScrapingManager.ScrapeAllData(htmlDoc, urlData.EventDate);
                        //Call the scraper method, this should check a different table, WorkerServiceLog, which defines the Worker Service Id, and the RaceDates it has
                        //Then generate a list of 'missing dates' from that.
                        //For example, if we had ran this 3 days ago, it should notice that we are missing data from today backwards,
                        //Now scraping todays races could be a little different to scraping historic races, for that reason I would think the Fetch Automator may need
                        //to call a different function if todays races are missing, and with that in mind, we may need a process to fetch results also if the results 
                        //are missing, a simple 'iscomplete' flag on each race should do the trick.
                        //Back to the original point, we ran this project 3 days ago, so we fetch races for today, yesterday and the day before, then it picks up
                        //on the last date before that, and fetches those races too.


                        //Lastly, delete all resolved errors...
                        Console.WriteLine("Auto Retriever Complete");
                        await _webScrapingManager.AddAutoretrieverLog(urlData.EventDate, true, "Success");
                    }
                    catch(Exception ex)
                    {
                        await _webScrapingManager.AddAutoretrieverLog(urlData.EventDate, false, ex.Message);
                        //worker.Start = false; TEMPORARY -> Enable when the Autoretriever is ready
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
