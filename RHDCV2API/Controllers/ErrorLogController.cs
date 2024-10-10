using Microsoft.AspNetCore.Mvc;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace RHDCV2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorLogController : ControllerBase
    {
        private readonly IErrorLogManager _errorManager;
        public ErrorLogController(IErrorLogManager errorManager)
        {
            _errorManager = errorManager;
        }

        [HttpGet]
        [Route("GetUnresolved")]
        public List<ErrorLogModel> GetUnresolved()
        {
            return _errorManager.GetErrors();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task Resolve(ErrorLogModel model)
        {
            await _errorManager.Resolve(model);
        }
    }
}
