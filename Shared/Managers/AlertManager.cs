using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Enums;
using Shared.Managers.Interfaces;
using Shared.Models.ApiModels;

namespace Shared.Managers
{
    public class AlertManager : IAlertManager
    {
        private readonly RHDCV2Context _context;

        public AlertManager(RHDCV2Context context)
        {
            _context = context;
        }

        public List<AlertModel> GetAlerts()
        {
            return _context.tb_alert.Where(x => !x.Resolved).Select(y => new AlertModel() 
            {
                Id = y.Id,
                DateLogged = y.DateLogged,
                Message = y.Message,
                Resolved = y.Resolved,
                Type = y.Type.ToString(),
                Endpoint = GetEndpointForAlertType(y.Type, y.Id)
            }).ToList();
        }

        public async Task CreateAlert(AlertType type, string message)
        {
            try
            {
                var error = new Alert()
                {
                    Type = type,
                    Message = message,
                    DateLogged = DateTime.Now,
                    Resolved = false
                };

                _context.tb_alert.Add(error);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string GetEndpointForAlertType(AlertType type, int id) 
        {
            switch (type)
            {
                case AlertType.NewCourse:
                    return "";
                default:
                    return "";
            }
        }

        public async Task Resolve(AlertModel model)
        {
            var alert = _context.tb_alert.FirstOrDefault(x => x.Id == model.Id);

            if (alert == null)
            {
                throw new Exception($"Could not identify alert with id of {model.Id}");
            }

            alert.Resolved = true;

            await _context.SaveChangesAsync();
        }

    }
}
