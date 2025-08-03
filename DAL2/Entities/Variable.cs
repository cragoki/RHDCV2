using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class Variable : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Precision(18, 4)]
        public decimal Importance { get; set; }
    }
}
