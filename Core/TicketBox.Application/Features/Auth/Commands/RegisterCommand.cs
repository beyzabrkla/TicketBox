using MediatR;

namespace TicketBox.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<bool> // Başarılı olup olmadığını dönecek
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
