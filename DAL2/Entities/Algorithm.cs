using DAL.Entities.Interfaces;
using DAL.Enums;

namespace DAL.Entities
{
    public class Algorithm : IEntity
    {
        public int Id { get; set; }
        public AlgorithmType AlgorithmType { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public int Version { get; set; }
    }
}
