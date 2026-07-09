using FluentValidation;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Tickets.Commands.Validators
{
    public class UpdateTicketCommandValidator :AbstractValidator<UpdateTicketCommand>
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public UpdateTicketCommandValidator(IGenericRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;

            RuleFor(x => x.TicketId).NotEmpty();
            RuleFor(x => x.PNR).NotEmpty().Length(6);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

            // Biletin statüsünün (kullanılmış mı, iptal mi?) kontrolünü yapıyoruz
            RuleFor(x => x.TicketId)
                .MustAsync(async (ticketId, ct) =>
                {
                    var ticket = await _ticketRepository.GetByIdAsync(ticketId);
                    if (ticket == null) return false;

                    // Eğer bilet iptal edilmişse veya zaten kullanılmışsa güncellemeye izin verme
                    return ticket.IsActive && !ticket.IsUsed;
                })
                .WithMessage("Bu bilet iptal edilmiş veya zaten kullanılmış olduğu için güncellenemez.");
            _ticketRepository = ticketRepository;
        }
    }
}
