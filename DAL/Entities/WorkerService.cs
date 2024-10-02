using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class WorkerService : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastRun { get; set; }
        public bool Enabled { get; set; }
        public bool Start { get; set; }
    }
}
