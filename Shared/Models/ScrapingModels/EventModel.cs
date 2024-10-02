namespace Shared.Models.ScrapingModels
{
    public class EventModel
    {
        public string CourseName { get; set; }
        public DateTime EventDate { get; set; }
        public List<RaceModel> Races { get; set; }
    }
}
