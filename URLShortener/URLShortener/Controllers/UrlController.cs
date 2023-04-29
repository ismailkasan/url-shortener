using Microsoft.AspNetCore.Mvc;
using URLShortener.Common;
using URLShortener.Services;

namespace UrlDemo.Controllers
{
    /// <summary>
    /// Url Controller endpoint
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        /// <summary>
        /// Create short url endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateShortUrl")]
        public async Task<ServiceResult> CreateShortUrl([FromBody] UrlRequestDto model)
        {
            return await _urlService.CreateShortUrl(model);
        }
    }
}
