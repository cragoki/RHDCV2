
namespace Shared.Managers
{
    public interface IMappingTableManager
    {
        Task<int> AddOrReturnAgeCategory(string age);
        Task<int> AddOrReturnDistanceCategory(string distance);
        Task<int> AddOrReturnGoingCategory(string going);
        Task<int> AddOrReturnHorse(string horseName);
        Task<int> AddOrReturnJockey(string jockeyName);
        Task<int> AddOrReturnTrainer(string trainerName);
    }
}