using DAL.Enums;

namespace Shared.Managers.Interfaces
{
    public interface IErrorLogManager
    {
        Task LogError(string tableName, string className, string methodName, ErrorType type, string stackTrace, string innerException, string message);
    }
}