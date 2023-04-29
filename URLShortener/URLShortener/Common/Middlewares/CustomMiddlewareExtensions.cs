
namespace URLShortener.Common
{
    /// <summary>
    /// Register custom middlewares to appBuilder.
    /// </summary>
    public static class CustomMiddlewareExtensions
    {
        /// <summary>
        /// Register "ExceptionMiddleware" to appBuilder.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// Register "ConfigureCustomRedirectionMiddleware" to appBuilder.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomRedirectionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RedirectionMiddleware>();
        }
    }
}
