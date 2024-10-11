using Shared.Models.ApiModels;

namespace Shared.Managers.Interfaces
{
    public interface IRaceManager
    {
        List<RaceModel> GetRacesForEvent(int eventId);
    }
}