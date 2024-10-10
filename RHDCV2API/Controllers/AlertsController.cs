using Microsoft.AspNetCore.Mvc;
using Shared.Managers;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace RHDCV2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertManager _alertManager;
        public AlertsController(IAlertManager alertManager)
        {
            _alertManager = alertManager;
        }

        [HttpGet]
        [Route("GetUnresolved")]
        public List<AlertModel> GetUnresolved()
        {
            return _alertManager.GetAlerts();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Resolve(AlertModel model)
        {
            await _alertManager.Resolve(model);
        }
    }
}

