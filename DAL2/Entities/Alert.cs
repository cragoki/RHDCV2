using DAL.Entities.Interfaces;
using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Alert : IEntity
    {
        public int Id { get; set; }
        public AlertType Type { get; set; }
        [MaxLength(2500)]
        public string? Message { get; set; }
        public DateTime DateLogged { get; set; }
        public bool Resolved { get; set; }

    }
}
