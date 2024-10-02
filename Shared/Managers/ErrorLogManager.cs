using Shared.Managers.Interfaces;
using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Enums;

namespace Shared.Managers
{
    public class ErrorLogManager : IErrorLogManager
    {
        private readonly RHDCV2Context _context;

        public ErrorLogManager(RHDCV2Context context)
        {
            _context = context;
        }

        public async Task LogError(string tableName, string className, string methodName, ErrorType type, string stackTrace, string innerException, string message)
        {
            try
            {
                var error = new ErrorLog()
                {
                    TableName = tableName,
                    ClassName = className,
                    MethodName = methodName,
                    ErrorType = type,
                    Stacktrace = stackTrace,
                    InnerException = innerException,
                    Message = message,
                    Resolved = false
                };

                _context.Add(error);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
