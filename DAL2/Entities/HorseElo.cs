using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class HorseElo : IEntity
    {
        public int Id { get; set; }
        public int VariableId { get; set; }
        public virtual Variable VariableEntity { get; set; }
        public int Elo { get; set; }
        public int? NumberOfRaces { get; set; }
        public DateTime? LastUpdated { get; set; }

    }
}
