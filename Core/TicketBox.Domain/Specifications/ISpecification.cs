using System.Linq.Expressions;

namespace TicketBox.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        // Sayfalama için yeni özellikler
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
