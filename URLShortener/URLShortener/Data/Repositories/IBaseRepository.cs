using URLShortener.Common;

namespace URLShortener.Data
{
    /// <summary>
    /// Generic Base Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Generic AddAsync method
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult<T>> AddAsync(T entity);
    }
}
