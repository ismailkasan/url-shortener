using URLShortener.Common;

namespace URLShortener.Data
{
    public interface IUrlRepository<T>: IBaseRepository<T> where T : class
    {
        Task<ServiceResult<T>> GetAsync(string segment);
        Task<ServiceResult<bool>> AnyAsync(string segment);       
    }
}
