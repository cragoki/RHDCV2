namespace Shared.Helpers
{
    public static class RaceCalculationHelper
    {
        public static int GetNumberOfPlacedHorses(int numberOfHorses)
        {
            if (numberOfHorses > 1 && numberOfHorses <= 4)
            {
                return 1;
            }
            else if (numberOfHorses >= 5 && numberOfHorses <= 7)
            {
                return 2;
            }
            else if (numberOfHorses >= 8 && numberOfHorses <= 15)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public static decimal CalculateWinAccuracyPercentage(List<bool> winAccuracy) 
        {
            return FormatHelper.ToTwoPlaces(winAccuracy.Any() ? (decimal)winAccuracy.Count(value => value) / winAccuracy.Count * 100 : 0);
        }

        public static decimal CalculatePlaceAccuracyPercentage(List<decimal> placeAccuracy)
        {
            return FormatHelper.ToTwoPlaces(placeAccuracy.Any() ? placeAccuracy.Average() : 0);
        }
    }
}
