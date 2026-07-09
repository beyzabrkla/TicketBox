using System.Linq.Expressions;
using TicketBox.Domain.Specifications;

namespace TicketBox.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        // Specification desteği (Tüm filtreleme işlerini burada yapacağız)
        Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
