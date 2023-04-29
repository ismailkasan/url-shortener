using System.Net;
using URLShortener.Data;

namespace URLShortener.Common
{
    public class RedirectionMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUrlRepository<UrlModel> urlRepository)
        {
            if (!httpContext.Request.Path.ToString().Contains("api")) // is an api request ?
            {
                await HandleRedirectionAsync(httpContext, urlRepository).ConfigureAwait(false);
            }
            else
            {
                await _next(httpContext);
            }
        }

        private static async Task HandleRedirectionAsync(HttpContext context, IUrlRepository<UrlModel> urlRepository)
        {
            var segment = context.Request.Path.ToUriComponent().Trim('/');

            if (segment.Length > Constant.ShortLinkSegmentLength)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("Short url is not valid! Short url segment length should be 6!");
            }
            else
            {
                var result = await urlRepository.GetAsync(segment);
                if (result.ResultType != ResultType.Success)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync("Short url is not found!");
                }
                else
                {
                    context.Response.Redirect(result.Data?.LongUrl ?? "/");
                }
            }
            await Task.CompletedTask;
        }
    }
}
