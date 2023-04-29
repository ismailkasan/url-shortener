namespace URLShortener.Common
{
    /// <summary>
    /// Response model for create short link endpoint.
    /// </summary>
    public class UrlResponseDto
    {
        /// <summary>
        /// Created short url.
        /// </summary>
        public string? ShortUrl { get; set; }
    }
}
