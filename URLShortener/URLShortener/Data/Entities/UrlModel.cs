namespace URLShortener.Data
{
    public class UrlModel
    {
        public UrlModel(string longUrl, string segment)
        {
            LongUrl = longUrl;
            Segment = segment;  
            IsDeleted = false;
        }

        public long Id { get; set; }
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
        public string Segment { get; set; }
        public bool IsDeleted { get; set; }
    }
}
