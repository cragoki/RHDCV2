using Shared.Models.ApiModels;

namespace Shared.Managers.Interfaces
{
    public interface IRaceHorseManager
    {
        List<RaceHorseModel> GetHorsesForRace(int raceId);
    }
}