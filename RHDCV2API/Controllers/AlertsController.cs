using Microsoft.AspNetCore.Mvc;
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
        public List<AlertModel> GetUnresolved()
        {
            return _alertManager.GetAlerts();
        }
    }
}

