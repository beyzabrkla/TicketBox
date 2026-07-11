using MediatR;
using Microsoft.AspNetCore.Identity;
using TicketBox.Application.Features.Auth.Commands;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            return result.Succeeded;
        }
    }
}
