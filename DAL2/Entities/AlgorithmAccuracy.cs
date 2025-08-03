using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class AlgorithmAccuracy : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        [Precision(18, 4)]
        public decimal Accuracy { get; set; }
    }
}
