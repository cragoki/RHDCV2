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
        public Race RaceEntity { get; set; }
        public int HorseId { get; set; }
        public Horse HorseEntity { get; set; }
        [Precision(18, 4)]
        public decimal Weight { get; set; }
        public int Age { get; set; }
        public int JockeyId { get; set; }
        public Jockey JockeyEntity { get; set; }
        public int TrainerId { get; set; }
        public Trainer TrainerEntity { get; set; }
        [Precision(18, 2)]
        public decimal? Odds { get; set; }
        [MaxLength(100)]
        public string? DistanceBetween { get; set; }
        [Precision(18, 2)]
        public decimal? Time { get; set; }
        public int? Position { get; set; }
    }
}
