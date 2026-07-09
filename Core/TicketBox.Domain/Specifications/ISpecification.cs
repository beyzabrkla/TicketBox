using System.Linq.Expressions;

namespace TicketBox.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //filtreleme kriteri
        List<Expression<Func<T, object>>> Includes { get; } //ilişkili verileri yüklemek için kullanılacak ifadeler
    }
}
