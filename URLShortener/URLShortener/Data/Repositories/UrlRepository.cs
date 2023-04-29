using LiteDB;
using URLShortener.Common;

namespace URLShortener.Data
{
    /// <summary>
    ///  Url repository is responsobla to handle database operations.
    /// </summary>
    public class UrlRepository : IUrlRepository<UrlModel>
    {
        /// <summary>
        /// Url collection
        /// </summary>
        private ILiteCollection<UrlModel> _urlModelCollection;

        /// <summary>
        /// Constructor of UrlRepository
        /// </summary>
        /// <param name="httpContext"></param>
        /// <exception cref="NullReferenceException"></exception>
        public UrlRepository(IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext == null)
            {
                throw new NullReferenceException("IHttpContextAccessor.HttpContext is null!");
            }
            var db = httpContext.HttpContext.RequestServices.GetRequiredService<ILiteDatabase>();
            _urlModelCollection = db.GetCollection<UrlModel>(BsonAutoId.Int64);
        }

        /// <summary>
        /// Gets the url from database according to segment.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns>Task<ServiceResult<UrlModel>></returns>
        /// <exception cref="NullReferenceException"></exception>
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

        /// <summary>
        /// Checks the Url exist or not according to segment.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns>Task<ServiceResult<bool>></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<ServiceResult<bool>> AnyAsync(string segment)
        {
            if (_urlModelCollection == null)
            {
                throw new NullReferenceException("_urlModelCollection is null!");
            }

            bool exist = _urlModelCollection.Exists(a => a.Segment == segment);
            return await Task.FromResult(new ServiceResult<bool>(exist));
        }

        /// <summary>
        ///  Adds Url to db and return ServiceResult<UrlModel> model.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task<ServiceResult<UrlModel>></returns>
        /// <exception cref="NullReferenceException"></exception>
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
    }
}
