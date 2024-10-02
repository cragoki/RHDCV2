using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using DAL.Enums;

namespace DAL.Entities
{
    public class Race : IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DaysEvent EventEntity { get; set; }
        public int Class { get; set; }
        public string? RaceUrl { get; set; }
        public RaceType RaceType { get; set; }
        public int AgeCategoryId { get; set; }
        public AgeCategory AgeCategoryEntity { get; set; }
        public int GoingCategoryId { get; set; }
        public GoingCategory GoingCategoryEntity { get; set; }
        public int DistanceCategoryId { get; set; }
        public DistanceCategory DistanceCategoryEntity { get; set; }
        public DateTime RaceDate { get; set; }
    }
}
