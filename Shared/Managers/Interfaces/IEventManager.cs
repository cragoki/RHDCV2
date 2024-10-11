using Shared.Models.ApiModels;

namespace Shared.Managers.Interfaces
{
    public interface IEventManager
    {
        List<EventModel> GetDaysEvents(DateTime date);
    }
}