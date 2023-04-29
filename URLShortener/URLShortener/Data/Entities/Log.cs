using System.ComponentModel.DataAnnotations;

namespace URLShortener.Data
{
    /// <summary>
    /// Log entity
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Id auto incremented
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Log message has length of 2.147.483.647
        /// </summary>
        [StringLength(int.MaxValue)]
        public string? Message { get; set; }

        /// <summary>
        /// Log stack trace has length of 2.147.483.647
        /// </summary>
        [StringLength(int.MaxValue)]
        public string? StackTrace { get; set; }

        /// <summary>
        /// MethodName is a uniqe generated Guid value.
        /// </summary>
        public string? MethodName { get; set; }

        /// <summary>
        /// Url of Request.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// Log created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
