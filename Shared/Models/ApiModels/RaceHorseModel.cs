namespace Shared.Models.ApiModels
{
    public class RaceHorseModel
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public string? HorseName { get; set; }
        public decimal Weight { get; set; }
        public int Age { get; set; }
        public string? JockeyName { get; set; }
        public string? TrainerName { get; set; }
        public decimal? Odds { get; set; }
        public string? DistanceBetween { get; set; }
        public decimal? Time { get; set; }
        public int? Position { get; set; }
        public string? AttireCategory { get; set; }
    }
}
