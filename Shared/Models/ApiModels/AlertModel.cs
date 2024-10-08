namespace Shared.Models.ApiModels
{
    public class AlertModel
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public DateTime DateLogged { get; set; }
        public bool Resolved { get; set; }
    }
}
