namespace URLShortener.Common
{
    /// <summary>
    /// Service Result for all endpoints.
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// ServiceResult ResultType
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        /// ServiceResult message
        /// </summary>
        public string Message { get; set; }

        public ServiceResult(string message = "", ResultType state = ResultType.Success)
        {
            ResultType = state;
            Message = message;
        }
    }
    /// <summary>
    /// Generic ServiceResult with messages and ResultType enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        /// Generic data
        /// </summary>
        public T? Data { get; set; }

        public ServiceResult(T? result, string message = "", ResultType state = ResultType.Success)
            : base(message, state)
        {
            Data = result;
        }
    }
}
