using DAL.Enums;

namespace Shared.Models.ScrapingModels
{
    public class RaceModel
    {
        public string? Class { get; set; }
        public RaceType RaceType { get; set; }
        public string? AgeCategoryName { get; set; }
        public string? GoingCategoryName { get; set; }
        public string? DistanceCategoryName { get; set; }
        public List<RaceHorseModel>? RaceHorses { get; set; }
        public string? RaceUrl { get; set; }

    }
}
