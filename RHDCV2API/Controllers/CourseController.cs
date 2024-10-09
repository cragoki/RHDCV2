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
        public List<RaceCourseModel> GetRaceCourses()
        {
            return _raceCourseManager.GetCourses();
        }
    }
}
