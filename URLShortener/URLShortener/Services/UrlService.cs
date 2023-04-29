using URLShortener.Common;
using URLShortener.Data;

namespace URLShortener.Services
{
    /// <summary>
    /// URl business service
    /// </summary>
    public class UrlService : IUrlService
    {
        /// <summary>
        /// Url repository object
        /// </summary>
        private readonly IUrlRepository<UrlModel> _urlModelRepository;

        /// <summary>
        /// HttpContextAccessor object
        /// </summary>
        private readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// Constructor of Url Service
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="httpContext"></param>
        public UrlService(IUrlRepository<UrlModel> repository, IHttpContextAccessor httpContext)
        {
            _urlModelRepository = repository;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Create a new short url and insert into database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Task<ServiceResult<UrlResponseDto>></returns>
        public async Task<ServiceResult<UrlResponseDto>> CreateShortUrl(UrlRequestDto model)
        {
            if (!Uri.TryCreate(model.LongUrl, UriKind.Absolute, out var validLongUrl))
            {
                return await Task.FromResult(new ServiceResult<UrlResponseDto>(null, "Url is invalid.", ResultType.Error));
            }

            UrlModel newUrl;

            // Custom url
            if (model.IsCustomUrl.HasValue && model.IsCustomUrl.Value)
            {
                string shortUrl = $"{_httpContext.HttpContext?.Request.Scheme}://{_httpContext.HttpContext?.Request.Host}/{model.CustomSegment}";

                newUrl = new UrlModel(validLongUrl.ToString(), model.CustomSegment)
                {
                    ShortUrl = shortUrl
                };
            }
            else
            {
                //:TODO We can use two different algorith to create a uniqe key.

                //string uniqeSegment = await CreateAndGuaranteeGuid();
                string uniqeSegment = await GenerateStringKey();
                string shortUrl = $"{_httpContext.HttpContext?.Request.Scheme}://{_httpContext.HttpContext?.Request.Host}/{uniqeSegment}";

                newUrl = new UrlModel(model.LongUrl, uniqeSegment)
                {
                    ShortUrl = shortUrl
                };
            }

            // add to db
            await _urlModelRepository.AddAsync(newUrl);

            var dto = new UrlResponseDto
            {
                ShortUrl = newUrl.ShortUrl,
            };

            return await Task.FromResult(new ServiceResult<UrlResponseDto>(dto, "Short url created successfully!"));
        }

        /// <summary>
        /// Generate uniqe new hashes that have length of 6 with 16.777.216 possibilities.
        /// And checks this segment whether exist in database or does not exist.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>Task<string></returns>
        private async Task<string> CreateAndGuaranteeGuid(string? guid = null)
        {
            if (guid == null)
            {
                string newGuid = Helpers.CreateGuid(Constant.ShortLinkSegmentLength);
                return await CreateAndGuaranteeGuid(newGuid);
            }

            var exist = await _urlModelRepository.AnyAsync(guid);

            if (exist.Data) // guid was exist. Create a new one.
            {
                string newGuid = Helpers.CreateGuid(Constant.ShortLinkSegmentLength);
                return await CreateAndGuaranteeGuid(newGuid);
            }
            else
            {
                return guid;
            }
        }

        /// <summary>
        /// Generates uniqe url segment and checks this segment whether exist in database or does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task<string></returns>
        private async Task<string> GenerateStringKey(string? key = null)
        {
            if (key == null)
            {
                string newKey = Helpers.CreateUniqeStrig(6);
                return await GenerateStringKey(newKey);
            }

            var exist = await _urlModelRepository.AnyAsync(key);
            if (exist.Data) // guid was exist. Create a new one.
            {
                string newKey = Helpers.CreateUniqeStrig(6);

                return await GenerateStringKey(newKey);
            }
            else
            {
                return key;
            }
        }
    }
}
