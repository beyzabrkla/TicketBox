using System.Linq.Expressions;

namespace TicketBox.Application.Features.Common.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //kriter bazlı filtreleme
        List<Expression<Func<T, object>>> Includes { get; } //birden fazla tabloyu join etmek için
    }
}
