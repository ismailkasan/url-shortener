using URLShortener.Common;

namespace URLShortener.Data
{
    /// <summary>
    ///  Generic Url Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUrlRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Generic GetAsync method for url.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        Task<ServiceResult<T>> GetAsync(string segment);

        /// <summary>
        /// Generic AnyAsync method for url.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        Task<ServiceResult<bool>> AnyAsync(string segment);
    }
}
