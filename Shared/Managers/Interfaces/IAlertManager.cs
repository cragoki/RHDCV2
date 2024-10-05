using DAL.Enums;

namespace Shared.Managers.Interfaces
{
    public interface IAlertManager
    {
        Task CreateAlert(AlertType type, string message);
    }
}