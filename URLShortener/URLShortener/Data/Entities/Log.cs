using System.ComponentModel.DataAnnotations;

namespace URLShortener.Data
{
    public class Log
    {
        public long Id { get; set; }
        public LogLevel LogLevel { get; set; }

        [StringLength(int.MaxValue)]
        public string? Message { get; set; }

        [StringLength(int.MaxValue)]
        public string? StackTrace { get; set; }
        public string? MethodName { get; set; }
        public string? Url { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
