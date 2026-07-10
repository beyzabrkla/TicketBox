using MediatR;

namespace TicketBox.Application.Features.Events.Commands
{
    public class CreateEventCommand : IRequest<Unit>  //MediatR dan miras alıyor
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EventDate { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public decimal? Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}
