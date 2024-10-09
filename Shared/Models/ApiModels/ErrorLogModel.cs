using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models.ApiModels
{
    public class ErrorLogModel
    {
        public int Id { get; set; }
        public string? TableName { get; set; }
        public string? ClassName { get; set; }
        public string? MethodName { get; set; }
        public string? ErrorType { get; set; }
        public string? Stacktrace { get; set; }
        public string? InnerException { get; set; }
        public string? Message { get; set; }
        public DateTime Date { get; set; }

    }
}
