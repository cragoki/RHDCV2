using Shared.Models.ApiModels;

namespace Shared.Managers.Interfaces
{
    public interface IRaceCourseManager
    {
        List<RaceCourseModel> GetCourses();
    }
}