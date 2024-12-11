using DAL.Entities;

namespace Shared.Helpers
{
    public static class AlgorithmHelper
    {

        public static IQueryable<Race> FilterRaces(AlgorithmVariable settings, IQueryable<Race> races)
        {
            if (!settings.IncludeAllWeather)
            {
                races = races.Where(x => !x.EventEntity.RaceCourse.IsAllWeather ?? true);
            }

            races = races.Where(x => x.Class <= settings.ClassLimit);

            return races;
        }
    }
}
