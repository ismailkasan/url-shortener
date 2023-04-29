using LiteDB;
using URLShortener.Common;

namespace URLShortener.Data
{
    /// <summary>
    /// Log repository is responsobla to handle database operations.
    /// </summary>
    public class LogRepository : IBaseRepository<Log>
    {
        /// <summary>
        /// Log collection
        /// </summary>
        private ILiteCollection<Log>? _logCollection;

        /// <summary>
        /// Constructor of LogRepository
        /// </summary>
        /// <param name="httpContext"></param>
        public LogRepository(IHttpContextAccessor httpContext)
        {
            var db = httpContext.HttpContext?.RequestServices.GetRequiredService<ILiteDatabase>();
            _logCollection = db?.GetCollection<Log>(BsonAutoId.Int64);
        }

        /// <summary>
        /// Adds log to db and return ServiceResult<Log> model.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task<ServiceResult<Log>></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<ServiceResult<Log>> AddAsync(Log entity)
        {
            if (_logCollection == null)
            {
                throw new NullReferenceException("HttpContext is null!");
            }

            long newId = _logCollection.Insert(entity);
            entity.Id = newId;
            return await Task.FromResult(new ServiceResult<Log>(entity));
        }
    }
}
