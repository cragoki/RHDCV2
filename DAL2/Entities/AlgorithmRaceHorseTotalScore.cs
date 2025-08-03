using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class AlgorithmRaceHorseTotalScore : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public int RaceHorseId { get; set; }
        public virtual RaceHorse RaceHorseEntity { get; set; }
        [Precision(18, 4)]
        public decimal TotalScore { get; set; }
        [Precision(18, 4)]
        public decimal RequiredOdds { get; set; }
        public int MaxPlace { get; set; }
        public bool IsCorrect { get; set; }
    }
}
