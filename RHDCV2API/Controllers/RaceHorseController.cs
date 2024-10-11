using Microsoft.AspNetCore.Mvc;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace RHDCV2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaceHorseController : ControllerBase
    {
        private readonly IRaceHorseManager _raceHorseManager;
        public RaceHorseController(IRaceHorseManager raceHorseManager)
        {
            _raceHorseManager = raceHorseManager;
        }

        [HttpGet]
        [Route("GetHorsesForRace")]
        public List<RaceHorseModel> GetHorsesForRace(int raceId)
        {
            return _raceHorseManager.GetHorsesForRace(raceId);
        }
    }
}
