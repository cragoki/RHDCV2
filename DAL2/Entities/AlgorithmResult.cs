using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class AlgorithmResult : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public int VariableId { get; set; }
        public virtual Variable VariableEntity { get; set; }
        public int RaceHorseId { get; set; }
        public virtual RaceHorse RaceHorseEntity { get; set; }
        [Precision(18, 4)]
        public decimal Score { get; set; }
        [Precision(18, 4)]
        public decimal AdjustedScore { get; set; }
    }
}
