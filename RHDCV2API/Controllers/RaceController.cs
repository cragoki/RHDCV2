using Microsoft.AspNetCore.Mvc;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace RHDCV2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly IRaceManager _raceManager;
        public RaceController(IRaceManager raceManager)
        {
            _raceManager = raceManager;
        }

        [HttpGet]
        [Route("GetRacesForEvent")]
        public List<RaceModel> GetRacesForEvent(int eventId)
        {
            return _raceManager.GetRacesForEvent(eventId);
        }
    }
}
