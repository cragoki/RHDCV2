using Shared.Managers.Interfaces;
using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Enums;
using Shared.Models.ApiModels;

namespace Shared.Managers
{
    public class ErrorLogManager : IErrorLogManager
    {
        private readonly RHDCV2Context _context;

        public ErrorLogManager(RHDCV2Context context)
        {
            _context = context;
        }

        public List<ErrorLogModel> GetErrors()
        {
            return _context.tb_error_log.Where(x => !x.Resolved).Select(y => new ErrorLogModel()
            {
                Id = y.Id,
                ClassName = y.ClassName,
                Stacktrace = y.Stacktrace,
                ErrorType = y.ErrorType.ToString(),
                InnerException = y.InnerException,
                Message = y.Message,
                MethodName = y.MethodName,
                TableName = y.TableName,
                Date = y.Date
            }).ToList();
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
                    Resolved = false,
                    Date = DateTime.UtcNow
                };

                _context.tb_error_log.Add(error);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Resolve(ErrorLogModel model)
        {
            var error = _context.tb_error_log.FirstOrDefault(x => x.Id == model.Id);

            if (error == null)
            {
                throw new Exception($"Could not identify error with id of {model.Id}");
            }

            error.Resolved = true;

            await _context.SaveChangesAsync();
        }

    }
}
