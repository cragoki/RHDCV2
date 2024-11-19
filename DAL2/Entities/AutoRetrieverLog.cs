using DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class AutoRetrieverLog : IEntity
    {
        public int Id { get; set; }
        public DateTime DateRetrieved { get; set; }
        public bool Success { get; set; }
        [MaxLength(250)]
        public string Note { get; set; }
        public int Retries { get; set; }

    }
}
