using DAL.DbRHDCV2Context;
using DAL.Enums;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace Shared.Managers
{
    public class RaceCourseManager : IRaceCourseManager
    {
        private readonly RHDCV2Context _context;

        public RaceCourseManager(RHDCV2Context context)
        {
            _context = context;
        }

        public List<RaceCourseModel> GetCourses()
        {
            return _context.tb_race_course.Select(y => new RaceCourseModel()
            {
                Id = y.Id,
                SpeedType = y.SpeedType.ToString(),
                Grade = y.Grade,
                IsAllWeather = y.IsAllWeather,
                Name = y.Name,
                SurfaceType = y.SurfaceType.ToString(),
                SpeedTypes = Enum.GetNames(typeof(SpeedType)).ToList(),
                SurfaceTypes = Enum.GetNames(typeof(SurfaceType)).ToList()
            }).ToList();
        }
    }
}
