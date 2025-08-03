using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class AlgorithmConfiguration : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public int VariableId { get; set; }
        public virtual Variable VariableEntity { get; set; }
        [Precision(18, 4)]
        public decimal Importance { get; set; }
    }
}
