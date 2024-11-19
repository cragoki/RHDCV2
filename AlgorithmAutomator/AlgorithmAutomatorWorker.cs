using Microsoft.Extensions.Hosting;

namespace AlgorithmAutomator
{
    public class AlgorithmAutomatorWorker : BackgroundService
    {
        public AlgorithmAutomatorWorker()
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing RHDC AlgorithmAutomator");

        }
    }
}
