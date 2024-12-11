using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Enums;
using Shared.Algorithms.Interfaces;
using Shared.Helpers;
using Shared.Models.Algorithms;

namespace Shared.Algorithms
{
    public class BentersAlgorithm : IAlphabeticalAlgorithm
    {
        private readonly RHDCV2Context _context;

        public BentersAlgorithm(RHDCV2Context context)
        {
            _context = context;
        }

        public async Task<AlgorithmExecution> ExecuteAlgorithm()
        {
            var result = new AlgorithmExecution();
            List<bool> winAccuracy = new List<bool>();
            List<decimal> placeAccuracy = new List<decimal>();
            int numberOfRaces = 0;

            try
            {
                var algorithm = _context.tb_algorithm.FirstOrDefault(x => x.AlgorithmType == AlgorithmType.Alphabetical);
                var algorithmSettings = _context.tb_algorithm_variable.FirstOrDefault(x => x.AlgorithmId == algorithm.Id);

                //Get the last 6 months worth of races.
                var pastEvents = _context.tb_event.Where(x => x.Date >= DateTime.Now.AddMonths(-6)).Select(x => x.Id);
                var pastRaces = _context.tb_race.Where(x => pastEvents.Contains(x.EventId));

                //Check Algorithm Settings to filter down race list
                var filteredRaces = AlgorithmHelper.FilterRaces(algorithmSettings, pastRaces);

                foreach (var race in filteredRaces)
                {
                    var predictions = await PredictRace(race);

                    //Get Actual Placing Horses
                    var numberOfPlacedHorses = RaceCalculationHelper.GetNumberOfPlacedHorses(predictions.Count());
                    var actualResults = _context.tb_race_horse.Where(x => x.RaceId == race.Id).OrderBy(x => x.Position).Take(numberOfPlacedHorses);
                    var placedHorses = actualResults.Select(x => x.Id);
                    int correctPlaced = 0;
                    bool correctWinner = false;

                    if (!placedHorses.Any()) 
                    {
                        continue;
                    }

                    if (placedHorses.First() == predictions.First().Id) 
                    {
                        correctWinner = true;
                        predictions.Remove(predictions.First());
                    }
                    foreach (var prediction in predictions.Take(numberOfPlacedHorses))
                    {
                        if (placedHorses.Contains(prediction.Id))
                        {
                            correctPlaced++;
                        }
                    }

                    winAccuracy.Add(correctWinner);
                    if (numberOfPlacedHorses > 0)
                    {
                        decimal percentageCorrect = (decimal)correctPlaced / numberOfPlacedHorses * 100;
                        placeAccuracy.Add(percentageCorrect);
                    }
                    else
                    {
                        placeAccuracy.Add(0);
                    }

                    numberOfRaces++;
                }

                result.AlgorithmId = algorithm.Id;
                result.NumberOfRaces = numberOfRaces;
                result.Date = DateTime.Now;
                result.PlaceAccuracy = RaceCalculationHelper.CalculatePlaceAccuracyPercentage(placeAccuracy);
                result.WinAccuracy = RaceCalculationHelper.CalculateWinAccuracyPercentage(winAccuracy);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<List<AlgorithmHorseRankingModel>> PredictRace(Race race)
        {
            var result = new List<AlgorithmHorseRankingModel>();

            try
            {
                var raceHorses = _context.tb_race_horse.Where(x => x.RaceId == race.Id);

                foreach (var horse in raceHorses)
                {

                    //Here we will split down into the various variables
                    var orderedList = raceHorses.OrderBy(x => x.HorseEntity.Name);
                    int points = orderedList.Count();


                    result.Add(new AlgorithmHorseRankingModel()
                    {
                        Id = horse.Id,
                        Name = horse.HorseEntity.Name,
                        Points = points,
                        Odds = horse.Odds
                    });
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
