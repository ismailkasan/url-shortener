namespace URLShortener.Data
{
    /// <summary>
    /// Url entity
    /// </summary>
    public class UrlModel
    {
        public UrlModel(string longUrl, string segment)
        {
            LongUrl = longUrl;
            Segment = segment;
            IsDeleted = false;
        }

        /// <summary>
        /// Id auto incremented
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Short form of url.
        /// </summary>
        public string ShortUrl { get; set; }
        /// <summary>
        /// Original long url
        /// </summary>
        public string LongUrl { get; set; }
        /// <summary>
        /// Uniqe identifier for short urls.
        /// </summary>
        public string Segment { get; set; }
        /// <summary>
        /// Status of soft deletion
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
