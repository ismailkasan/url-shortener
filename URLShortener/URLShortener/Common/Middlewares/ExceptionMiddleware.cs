using Newtonsoft.Json;
using System.Net;
using URLShortener.Data;
namespace URLShortener.Common
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IBaseRepository<Log> logRepository)
        {
            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, logRepository).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IBaseRepository<Log> logRepository)
        {
            try
            {

                // create guid
                string errorCode = Helpers.CreateGuid(8);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.Headers.Add("Application-Error", errorCode);
                context.Response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");

                Log log = new()
                {

                    LogLevel = LogLevel.Critical,
                    Message = string.Format("{0} -*- {1}", exception.Source, exception.Message),
                    StackTrace = exception.StackTrace,
                    MethodName = errorCode,
                    Url = context.Request.Path,
                    CreatedDate = DateTime.UtcNow
                };

                // add log to database
                await logRepository.AddAsync(log);

                var result = new ServiceResult("General Error Occured." + $"Error code: {errorCode}", ResultType.Error);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
                return;

            }
            catch
            {
                // İf an exception fires when to add log in database we can send log to third party service such Elastich Search Service.
             
                //TODO: add another service
                var result = new ServiceResult("General Error Occured.", ResultType.Error);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
                return;
            }
        }
    }

}
