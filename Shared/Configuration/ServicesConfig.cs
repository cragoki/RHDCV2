using Microsoft.Extensions.DependencyInjection;
using Shared.Managers;
using Shared.Managers.Interfaces;

namespace Shared.Configuration
{
    public static class ServicesConfig
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IErrorLogManager, ErrorLogManager>();
            services.AddSingleton<IWebScrapingManager, WebScrapingManager>();
            services.AddSingleton<IMappingTableManager, MappingTableManager>();
            services.AddSingleton<IDatabaseManager, DatabaseManager>();
            services.AddSingleton<IAlertManager, AlertManager>();
            services.AddSingleton<IRaceCourseManager, RaceCourseManager>();
            services.AddSingleton<IEventManager, EventManager>();
            services.AddSingleton<IRaceManager, RaceManager>();
            services.AddSingleton<IRaceHorseManager, RaceHorseManager>();
        }
    }
}
