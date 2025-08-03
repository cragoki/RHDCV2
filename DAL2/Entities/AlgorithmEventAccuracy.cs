using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class AlgorithmEventAccuracy : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public int EventId { get; set; }
        public virtual DaysEvent EventEntity { get; set; }
        public int NumberOfPicks { get; set; }
        public int NumberOfCorrectPicks { get; set; }
        [Precision(18, 4)]
        public decimal Accuracy { get; set; }
    }
}
