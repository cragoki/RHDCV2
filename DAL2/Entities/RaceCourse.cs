using DAL.Entities.Interfaces;
using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class RaceCourse : IEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public SurfaceType? SurfaceType { get; set; }
        public CourseType? CourseType { get; set; }
        public SpeedType? SpeedType { get; set; }
        public bool? IsAllWeather { get; set; }

    }
}
