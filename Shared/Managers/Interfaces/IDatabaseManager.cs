using Shared.Models.ScrapingModels;

namespace Shared.Managers.Interfaces
{
    public interface IDatabaseManager
    {
        Task AddEventAndRaceData(List<EventModel> events);
    }
}