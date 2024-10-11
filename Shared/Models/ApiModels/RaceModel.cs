namespace Shared.Models.ApiModels
{
    public class RaceModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int Class { get; set; }
        public string? RaceUrl { get; set; }
        public string? RaceType { get; set; }
        public string? AgeCategory { get; set; }
        public string? GoingCategory { get; set; }
        public string? DistanceCategory { get; set; }
        public DateTime RaceDate { get; set; }
    }
}
