using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class AlgorithmExecution : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public decimal WinAccuracy { get; set; }
        public decimal PlaceAccuracy { get; set; }
        public int NumberOfRaces { get; set; }
        public DateTime Date { get; set; }
    }
}
