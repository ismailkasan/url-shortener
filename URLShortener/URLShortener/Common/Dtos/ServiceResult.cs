namespace URLShortener.Common
{
    public class ServiceResult
    {
        public ResultType ResultType { get; set; }

        public string Message { get; set; }

        public ServiceResult(string message = "", ResultType state = ResultType.Success)
        {
            ResultType = state;
            Message = message;
        }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public ServiceResult(T? result, string message = "", ResultType state = ResultType.Success)
            : base(message, state)
        {
            Data = result;
        }
    }
}
