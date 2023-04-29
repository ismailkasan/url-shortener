using LiteDB;
using URLShortener.Common;

namespace URLShortener.Data
{
    public class UrlRepository : IUrlRepository<UrlModel>
    {
        private ILiteCollection<UrlModel> _urlModelCollection;
        public UrlRepository(IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext == null)
            {
                throw new NullReferenceException("IHttpContextAccessor.HttpContext is null!");
            }
            var db = httpContext.HttpContext.RequestServices.GetRequiredService<ILiteDatabase>();
            _urlModelCollection = db.GetCollection<UrlModel>(BsonAutoId.Int64);
        }
        public async Task<ServiceResult<UrlModel>> GetAsync(string segment)
        {
            if (_urlModelCollection == null)
            {
                throw new NullReferenceException("_urlModelCollection is null!");
            }

            var url = _urlModelCollection.FindOne(a => a.Segment == segment);
            if (url == null)
            {
                return await Task.FromResult(new ServiceResult<UrlModel>(null, "Url Not Found", ResultType.Error));
            }
            else
            {
                return await Task.FromResult(new ServiceResult<UrlModel>(url));
            }
        }
        public async Task<ServiceResult<bool>> AnyAsync(string segment)
        {
            if (_urlModelCollection == null)
            {
                throw new NullReferenceException("_urlModelCollection is null!");
            }

            bool exist = _urlModelCollection.Exists(a => a.Segment == segment);
            return await Task.FromResult(new ServiceResult<bool>(exist));
        }
        public async Task<ServiceResult<UrlModel>> AddAsync(UrlModel entity)
        {
            if (_urlModelCollection == null)
            {
                throw new NullReferenceException("_urlModelCollection is null!");
            }

            long newId = _urlModelCollection.Insert(entity);
            entity.Id = newId;
            return await Task.FromResult(new ServiceResult<UrlModel>(entity));
        }

        public async Task<ServiceResult<UrlModel>> GetAsync(long id)
        {
            if (_urlModelCollection == null)
            {
                throw new NullReferenceException("_urlModelCollection is null!");
            }

            var url = _urlModelCollection.FindById(id);
            return await Task.FromResult(new ServiceResult<UrlModel>(url));
        }
    }
}
