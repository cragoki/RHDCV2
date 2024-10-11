using Microsoft.Extensions.DependencyInjection;
using Shared.Managers;
using Shared.Managers.Interfaces;

namespace Shared.Configuration
{
    public static class ServicesConfig
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IErrorLogManager, ErrorLogManager>();
            services.AddScoped<IWebScrapingManager, WebScrapingManager>();
            services.AddScoped<IMappingTableManager, MappingTableManager>();
            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<IAlertManager, AlertManager>();
            services.AddScoped<IRaceCourseManager, RaceCourseManager>();
            services.AddScoped<IEventManager, EventManager>();
            services.AddScoped<IRaceManager, RaceManager>();
            services.AddScoped<IRaceHorseManager, RaceHorseManager>();
        }
    }
}
