using DAL.Enums;
using Microsoft.Extensions.Hosting;
using Shared.Managers.Interfaces;

namespace AlgorithmAutomator
{
    public class AlgorithmAutomatorWorker : BackgroundService
    {
        private readonly IAlgorithmManager _algorithmManager;
        public AlgorithmAutomatorWorker(IAlgorithmManager algorithmManager)
        {
            _algorithmManager = algorithmManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing RHDC AlgorithmAutomator");

            //For now a hard coded algorithm, will change this when it can be configured on the UI
            var result = await _algorithmManager.ExecuteAlgorithm(AlgorithmType.Alphabetical);

            Console.WriteLine("RHDC AlgorithmAutomator Complete Statistics Are:");
            Console.WriteLine($"Number Of Races: {result.NumberOfRaces}");
            Console.WriteLine($"Win Accuracy: {result.WinAccuracy}");
            Console.WriteLine($"Place Accuracy: {result.PlaceAccuracy}");

        }
    }
}
