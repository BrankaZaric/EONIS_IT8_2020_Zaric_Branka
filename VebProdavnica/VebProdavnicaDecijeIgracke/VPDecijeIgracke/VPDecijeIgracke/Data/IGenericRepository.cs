using Org.BouncyCastle.Tls;
using VPDecijeIgracke.Data.Specifications;

namespace VPDecijeIgracke.Data
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync (ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

    }
}
