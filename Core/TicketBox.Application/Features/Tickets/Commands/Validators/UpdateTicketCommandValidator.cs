using FluentValidation;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Tickets.Commands.Validators
{
    public class UpdateTicketCommandValidator :AbstractValidator<UpdateTicketCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTicketCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.TicketId).NotEmpty();
            RuleFor(x => x.PNR).NotEmpty().Length(6);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

            // Biletin statüsünün (kullanılmış mı, iptal mi?) kontrolünü yapıyoruz
            RuleFor(x => x.TicketId)
                .MustAsync(async (ticketId, ct) =>
                {
                    var ticket = await _context.Tickets.FindAsync(new object[] { ticketId },ct);
                    if (ticket == null) return false;

                    // Eğer bilet iptal edilmişse veya zaten kullanılmışsa güncellemeye izin verme
                    return ticket.IsActive && !ticket.IsUsed;
                })
                .WithMessage("Bu bilet iptal edilmiş veya zaten kullanılmış olduğu için güncellenemez.");
        }
    }
}
