using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class AlgorithmVariable : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public Algorithm AlgorithmEntity { get; set; }
        public bool IncludeAllWeather { get; set; }
        public int ClassLimit { get; set; }
    }
}
