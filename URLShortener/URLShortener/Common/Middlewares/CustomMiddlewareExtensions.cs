
namespace URLShortener.Common
{
    public static class CustomMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
        public static void ConfigureCustomRedirectionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RedirectionMiddleware>();
        }
    }
}
