using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public class AlgorithmRacePrediction : IEntity
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public virtual Algorithm AlgorithmEntity { get; set; }
        public int EventId { get; set; }
        public virtual DaysEvent EventEntity { get; set; }
        public int RaceId { get; set; }
        public virtual Race RaceEntity { get; set; }
        public int PickOneId { get; set; }
        public virtual AlgorithmRaceHorseTotalScore PickOneEntity { get; set; }
        public int PickTwoId { get; set; }
        public virtual AlgorithmRaceHorseTotalScore PickTwoEntity { get; set; }

    }
}
