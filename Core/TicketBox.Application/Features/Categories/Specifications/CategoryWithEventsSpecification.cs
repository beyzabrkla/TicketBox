using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Categories.Specifications
{
    public class CategoryWithEventsSpecification :BaseSpecification<Category>
    {
        public CategoryWithEventsSpecification(int CategoryId) 
        {
            //CategoryId parametresine göre filtreleme yap
            AddCriteria(c => c.CategoryId == CategoryId);

            //Kategoriye ait etkinlikleri dahil et 
            AddInclude(c => c.Events);
        }
    }
}
