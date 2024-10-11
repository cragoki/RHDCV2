namespace Shared.Models.ScrapingModels
{
    public class ScrapingEventModel
    {
        public string CourseName { get; set; }
        public DateTime EventDate { get; set; }
        public List<ScrapingRaceModel> Races { get; set; }
    }
}
