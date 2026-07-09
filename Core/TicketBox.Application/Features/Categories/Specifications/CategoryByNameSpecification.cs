using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Categories.Specifications
{
    public class CategoryByNameSpecification : BaseSpecification<Category>
    {
        public CategoryByNameSpecification(string Categoryname)
        {
            //categoryName parametresine göre filtreleme yap
            AddCriteria(c => c.CategoryName.Contains(Categoryname));
        }
    }
}
