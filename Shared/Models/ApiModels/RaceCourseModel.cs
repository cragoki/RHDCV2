namespace Shared.Models.ApiModels
{
    public class RaceCourseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurfaceType { get; set; }
        public string? CourseType { get; set; }
        public string? SpeedType { get; set; }
        public bool? IsAllWeather { get; set; }
        public List<string>? SurfaceTypes { get; set; }
        public List<string>? SpeedTypes { get; set; }
        public List<string>? CourseTypes { get; set; }

    }
}
