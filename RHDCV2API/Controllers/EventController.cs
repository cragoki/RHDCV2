using Microsoft.AspNetCore.Mvc;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace RHDCV2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventManager _eventManager;
        public EventController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet]
        [Route("GetEvents")]
        public List<EventModel> GetEvents(DateTime date)
        {
            return _eventManager.GetDaysEvents(date);
        }
    }
}
