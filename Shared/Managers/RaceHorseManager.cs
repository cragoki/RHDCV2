using Azure;
using DAL.DbRHDCV2Context;
using DAL.Enums;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace Shared.Managers
{
    public class RaceHorseManager : IRaceHorseManager
    {
        private readonly RHDCV2Context _context;

        public RaceHorseManager(RHDCV2Context context)
        {
            _context = context;
        }

        public List<RaceHorseModel> GetHorsesForRace(int raceId)
        {
            return _context.tb_race_horse.Where(x => x.RaceId == raceId).Select(y => new RaceHorseModel()
            {
                Id = y.Id,
                RaceId = y.RaceId,
                HorseName = y.HorseEntity.Name,
                Weight = y.Weight,
                Age = y.Age,
                JockeyName = y.JockeyEntity.Name,
                TrainerName = y.TrainerEntity.Name,
                Odds = y.Odds,
                DistanceBetween = y.DistanceBetweenCategoryId == null ? "" : y.DistanceBetweenCategoryEntity.Name,
                Time = y.Time,
                Position = y.Position,
                AttireCategory = y.AttireCategoryId == null ? "" : y.AttireCategoryEntity.Name


            }).ToList();
        }
    }
}
