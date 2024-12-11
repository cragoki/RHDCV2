using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class AlgorithmVariable : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public bool IncludeAllWeather { get; set; }
        public int ClassLimit { get; set; }

        #region Current Condition
        public bool CurrentConditionEnabled { get; set; }
        public int? CurrentConditionPoints { get; set; }

        public bool PerformanceInLastXRacesEnabled { get; set; }
        public int? PerformanceInLastXRacesTake { get; set; }

        public bool TimeSinceLastRaceEnabled { get; set; }
        public bool AgeOfHorseEnabled { get; set; }

        #endregion
    }
}
