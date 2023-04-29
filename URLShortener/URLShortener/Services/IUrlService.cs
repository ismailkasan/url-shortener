using URLShortener.Common;

namespace URLShortener.Services
{
    /// <summary>
    /// Response for Url business operations. 
    /// </summary>
    public interface IUrlService
    {
        /// <summary>
        /// Create short url.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ServiceResult<UrlResponseDto>> CreateShortUrl(UrlRequestDto model);
    }
}