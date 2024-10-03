using DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.MappingTables
{
    public class AttireCategory : IEntity
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
