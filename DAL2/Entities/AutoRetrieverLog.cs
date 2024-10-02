using DAL.Entities.Interfaces;
using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class AutoRetrieverLog : IEntity
    {
        public int Id { get; set; }
        public DateTime DateRetrieved { get; set; }
        public bool Success { get; set; }

    }
}
