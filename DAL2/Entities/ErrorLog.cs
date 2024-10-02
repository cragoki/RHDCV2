using DAL.Entities.Interfaces;
using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ErrorLog : IEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? TableName { get; set; }
        [MaxLength(100)]
        public string? ClassName { get; set; }
        [MaxLength(100)]
        public string? MethodName { get; set; }
        public ErrorType ErrorType { get; set; }
        public string? Stacktrace { get; set; }
        public string? InnerException { get; set; }
        public string? Message { get; set; }
        public bool Resolved { get; set; }
    }
}
