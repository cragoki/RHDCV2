using DAL.DbRHDCV2Context;
using DAL.Enums;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace Shared.Managers
{
    public class EventManager : IEventManager
    {
        private readonly RHDCV2Context _context;

        public EventManager(RHDCV2Context context)
        {
            _context = context;
        }

        public List<EventModel> GetDaysEvents(DateTime date)
        {
            return _context.tb_event.Where(x => x.Date.Date == date.Date).Select(y => new EventModel()
            {
                Id = y.Id,
                RaceCourse = y.RaceCourse.Name,
                Date = y.Date
            }).ToList();
        }
    }
}
