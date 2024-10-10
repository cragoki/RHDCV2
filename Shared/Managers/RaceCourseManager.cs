using DAL.DbRHDCV2Context;
using DAL.Enums;
using Shared.Helpers;
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

        public RaceCourseModel GetCourse(int id)
        {
            var course = _context.tb_race_course.FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new Exception($"Could not identify course with id of {id}");
            }

            return new RaceCourseModel()
            {
                Id = course.Id,
                SpeedType = course.SpeedType.ToString(),
                Grade = course.Grade,
                IsAllWeather = course.IsAllWeather,
                Name = course.Name,
                SurfaceType = course.SurfaceType.ToString(),
                SpeedTypes = Enum.GetNames(typeof(SpeedType)).ToList(),
                SurfaceTypes = Enum.GetNames(typeof(SurfaceType)).ToList()
            };
        }

        public async Task EditCourse(RaceCourseModel model)
        {
            var course = _context.tb_race_course.FirstOrDefault(x => x.Id == model.Id);

            if (course == null)
            {
                throw new Exception($"Could not identify course with id of {model.Id}");
            }

            course.SurfaceType = EnumHelper.ParseEnum<SurfaceType>(model.SurfaceType);
            course.Grade = model.Grade;
            course.IsAllWeather = model.IsAllWeather;
            course.SpeedType = EnumHelper.ParseEnum<SpeedType>(model.SpeedType);

            await _context.SaveChangesAsync();
        }
    }
}
