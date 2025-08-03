using DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.MappingTables
{
    public class Algorithm : IEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int Version { get; set; }

    }
}
