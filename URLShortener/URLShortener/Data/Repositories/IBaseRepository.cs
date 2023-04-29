using URLShortener.Common;

namespace URLShortener.Data
{
    public interface IBaseRepository<T> where T : class
    {
         Task<ServiceResult<T>> AddAsync(T entity);
    }
}
