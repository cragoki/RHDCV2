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
        public List<EventModel> GetEvents(string date)
        {

            if (String.IsNullOrEmpty(date))
            {
                throw new Exception("Date was empty");
            }
            DateTime dateTime = DateTime.Parse(date);

            return _eventManager.GetDaysEvents(dateTime);
        }
    }
}
