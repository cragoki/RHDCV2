using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class RaceHorse : IEntity
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public virtual Race RaceEntity { get; set; }
        public int HorseId { get; set; }
        public virtual Horse HorseEntity { get; set; }
        [Precision(18, 4)]
        public decimal Weight { get; set; }
        public int Age { get; set; }
        public int JockeyId { get; set; }
        public virtual Jockey JockeyEntity { get; set; }
        public int TrainerId { get; set; }
        public virtual Trainer TrainerEntity { get; set; }
        [Precision(18, 2)]
        public decimal? Odds { get; set; }
        public int? DistanceBetweenCategoryId { get; set; }
        public virtual DistanceBetweenCategory DistanceBetweenCategoryEntity { get; set; }
        [Precision(18, 2)]
        public decimal? Time { get; set; }
        public int? Position { get; set; }
        public int? AttireCategoryId { get; set; }
        public virtual AttireCategory AttireCategoryEntity { get; set; }

    }
}
