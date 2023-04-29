using LiteDB;
using URLShortener.Common;

namespace URLShortener.Data
{
    public class LogRepository : IBaseRepository<Log>
    {
        private ILiteCollection<Log>? _logCollection;
        public LogRepository(IHttpContextAccessor httpContext)
        {
            var db = httpContext.HttpContext?.RequestServices.GetRequiredService<ILiteDatabase>();
            _logCollection = db?.GetCollection<Log>(BsonAutoId.Int64);
        }
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
