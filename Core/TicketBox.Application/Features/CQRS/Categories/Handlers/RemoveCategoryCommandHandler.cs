using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class RemoveCategoryCommandHandler
    {
        private readonly TicketContext _context;

        public RemoveCategoryCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveCategoryCommand command)
        {
            var category = await _context.Categories.FindAsync(command.CategoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
