using DAL.DbRHDCV2Context;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace Shared.Managers
{
    public class RaceManager : IRaceManager
    {
        private readonly RHDCV2Context _context;

        public RaceManager(RHDCV2Context context)
        {
            _context = context;
        }

        public List<RaceModel> GetRacesForEvent(int eventId)
        {
            return _context.tb_race.Where(x => x.EventId == eventId).Select(y => new RaceModel()
            {
                Id = y.Id,
                EventId = y.EventId,
                Class = y.Class,
                RaceUrl = y.RaceUrl,
                RaceType = y.RaceType.ToString(),
                AgeCategory = y.AgeCategoryEntity.Name,
                GoingCategory = y.GoingCategoryEntity.Name,
                DistanceCategory = y.DistanceCategoryEntity.Name,
                RaceDate = y.RaceDate
            }).ToList();
        }
    }
}
