using Microsoft.EntityFrameworkCore;
using TicketBox.Domain.Specifications;

namespace TicketBox.Persistance.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            //Kriter (Where)
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // İlişkili verileri yükle
            if (spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            //Sıralama (OrderBy - Skip/Take)
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            //Sayfalama (Skip/Take en sonda olmalı)
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
    }
}
