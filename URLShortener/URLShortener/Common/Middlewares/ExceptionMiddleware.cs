using Newtonsoft.Json;
using System.Net;
using URLShortener.Data;
namespace URLShortener.Common
{
    /// <summary>
    /// Custom middleware is to catch all exceptions on whole application.Then, handles exceptions and return ServiceResult model.
    /// </summary>
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

        /// <summary>
        /// Handle exception and tries to add log to db.
        /// If it couldn't insert any logs to db, it can consume third part application services(file log,elastich search, vs.). 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="logRepository"></param>
        /// <returns>Task</returns>
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
                // if any exception fires here when it tries to add any logs in database, we can send logs to third party service such Elastich Search Service.
                //TODO: add another service
                var result = new ServiceResult("General Error Occured.", ResultType.Error);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
                return;
            }
        }
    }
}
