using MediatR;

namespace TicketBox.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RememberMe { get; set; }
    }
}
