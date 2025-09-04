using Shared.Constants;
using Shared.Managers.Interfaces;
using DAL.DbRHDCV2Context;
using DAL.Enums;
using Microsoft.Extensions.Hosting;

namespace TodaysRaces
{
    public class TodaysRacesWorker : BackgroundService
    {
        private readonly RHDCV2Context _context;
        private readonly IErrorLogManager _error;
        private readonly IWebScrapingManager _webScrapingManager;
        private readonly IDatabaseManager _dataManager;

        public TodaysRacesWorker(RHDCV2Context context, IErrorLogManager error, IWebScrapingManager webScrapingManager, IDatabaseManager dataManager) 
        {
            _context = context;
            _error = error;
            _webScrapingManager = webScrapingManager;
            _dataManager = dataManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Initializing {WorkerServiceConstants.TodaysRaces}");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking For Updates...");

                var worker = _context.tb_worker_service.FirstOrDefault(x => x.Name == WorkerServiceConstants.TodaysRaces);

                if (worker == null) 
                {
                    //Log Error 
                    await _error.LogError("tb_worker_service", "TodaysRacesWorker.cs", "ExecuteAsync", ErrorType.Setup, null!, null!, null!);
                    throw new Exception($"{WorkerServiceConstants.TodaysRaces} not found in the database");
                }

                if (!worker.Enabled) 
                {
                    Console.WriteLine($"{WorkerServiceConstants.TodaysRaces} has been disabled");
                    return;
                }

                if (worker.Start)
                {

                    try
                    {
                        await _dataManager.ClearTodaysRaces();
                        //Pass url into the scraper and hope for the best
                        var htmlDoc = await _webScrapingManager.Scrape("https://www.attheraces.com/racecards");
                        var scrapedData = await _webScrapingManager.ScrapeTodaysData(htmlDoc, DateTime.Now);

                        await _dataManager.AddEventAndRaceData(scrapedData, true);

                        Console.WriteLine($"{WorkerServiceConstants.TodaysRaces} Complete");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"{WorkerServiceConstants.TodaysRaces} Failed...");
                    }

                    worker.LastRun = DateTime.Now;
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                else 
                {
                    Console.WriteLine($"{WorkerServiceConstants.TodaysRaces} Sleeping...");
                }

                Thread.Sleep((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            }

        }

    }
}
