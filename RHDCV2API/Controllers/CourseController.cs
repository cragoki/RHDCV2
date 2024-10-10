using Microsoft.AspNetCore.Mvc;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace RHDCV2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IRaceCourseManager _raceCourseManager;
        public CourseController(IRaceCourseManager raceCourseManager)
        {
            _raceCourseManager = raceCourseManager;
        }

        [HttpGet]
        [Route("GetCourses")]
        public List<RaceCourseModel> GetRaceCourses()
        {
            return _raceCourseManager.GetCourses();
        }

        [HttpGet]
        [Route("[action]")]
        public RaceCourseModel GetRaceCourse(int id)
        {
            return _raceCourseManager.GetCourse(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task EditRaceCourse(RaceCourseModel model)
        {
            await _raceCourseManager.EditCourse(model);
        }
    }
}
