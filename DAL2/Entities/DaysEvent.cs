using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class DaysEvent : IEntity
    {
        public int Id { get; set; }
        public int RaceCourseId { get; set; }
        public virtual RaceCourse RaceCourse { get; set; }
        public DateTime Date { get; set; }
    }
}
