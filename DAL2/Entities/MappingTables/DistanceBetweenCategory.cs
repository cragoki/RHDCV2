using DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.MappingTables
{
    public class DistanceBetweenCategory : IEntity
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
