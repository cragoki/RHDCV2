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
                Type = y.Type.ToString()
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

    }
}
