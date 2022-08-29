using CadastroMotorista.Infra.Shared.Models;
using System;
using System.Text.Json;

namespace CadastroMotorista.Infra.Shared.Utils
{
    public static class Assistant
    {
        public static string FormatRequest(string method, object request, string client, string searchKey = "")
        {
            try
            {
                LogRequest log = new LogRequest()
                {
                    Id = Guid.NewGuid(),
                    Method = method,
                    Client = client,
                    LogDate = DateTime.Now,
                    Request = request
                };

                return JsonSerializer.Serialize(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FormatResponse(string method, object response, string searchKey = "")
        {
            try
            {
                LogResponse log = new LogResponse()
                {
                    Id = Guid.NewGuid(),
                    LogDate = DateTime.Now,
                    Method = method,
                    Response = response
                };

                return JsonSerializer.Serialize(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FormatError(string method, string errorMessage, string stackTrace, string innerException, string searchKey = "")
        {
            try
            {
                LogError log = new LogError()
                {
                    Id = Guid.NewGuid(),
                    LogDate = DateTime.Now,
                    Method = method,
                    ErrorMessage = errorMessage,
                    InnerException = innerException,
                    StackTrace = stackTrace,
                    SearchKey = searchKey
                };

                return JsonSerializer.Serialize(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}