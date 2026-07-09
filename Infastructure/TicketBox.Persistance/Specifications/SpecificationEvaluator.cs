using Microsoft.EntityFrameworkCore;
using TicketBox.Domain.Specifications;

namespace TicketBox.Persistance.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            //Kriter varsa filtrele (Where)
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            //Include listesi varsa ilişkili tabloları ekle
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
