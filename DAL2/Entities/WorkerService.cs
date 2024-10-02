using DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class WorkerService : IEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        public DateTime? LastRun { get; set; }
        public bool Enabled { get; set; }
        public bool Start { get; set; }
    }
}
