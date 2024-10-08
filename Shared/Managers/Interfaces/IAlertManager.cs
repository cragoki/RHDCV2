using DAL.Enums;
using Shared.Models.ApiModels;

namespace Shared.Managers.Interfaces
{
    public interface IAlertManager
    {
        Task CreateAlert(AlertType type, string message);
        List<AlertModel> GetAlerts();
    }
}