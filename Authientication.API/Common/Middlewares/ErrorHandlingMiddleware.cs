using  Authentication.Application.Common.Enums;
using  Authentication.Application.Common.Exceptions;
using  Authentication.Application.Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace  Authentication.API.Common.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ResponseVM response = new ResponseVM { StatusCode = HttpStatusCode.InternalServerError };

            switch (exception)
            {
                case ValidationException validationException:
                    response = ConstructValidationResponseMessage(validationException);
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    response = ConstructValidationResponseMessage(unauthorizedAccessException);
                    break;
                default:
                    response = ConstructGeneralException(exception);
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =(int)response.StatusCode;
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response, jsonSerializerSettings));
        }

        private ResponseVM ConstructValidationResponseMessage(UnauthorizedAccessException exception)
        {
            this.logger.LogError(exception.Message);
            return new ResponseVM { StatusCode = HttpStatusCode.Forbidden, Error = new string[] { exception.Message } };
        }

        private ResponseVM ConstructValidationResponseMessage(ValidationException exception)
        {
            this.logger.LogError(exception.Message);
            return new ResponseVM { StatusCode = HttpStatusCode.BadRequest, Error = exception.FailuresMessages };
        }
        private ResponseVM ConstructGeneralException(Exception exception)
        {
            this.logger.LogError(exception.ToString());
            return new ResponseVM { StatusCode = HttpStatusCode.InternalServerError, Error = exception.Message };
        }
    }
}
