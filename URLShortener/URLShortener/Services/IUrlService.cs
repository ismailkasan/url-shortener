using URLShortener.Common;

namespace URLShortener.Services
{
    public interface IUrlService
    {
        public Task<ServiceResult<UrlResponseDto>> CreateShortUrl(UrlRequestDto model);
    }
}