using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Enums;
using Shared.Managers.Interfaces;

namespace Shared.Managers
{
    public class AlertManager : IAlertManager
    {
        private readonly RHDCV2Context _context;

        public AlertManager(RHDCV2Context context)
        {
            _context = context;
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
