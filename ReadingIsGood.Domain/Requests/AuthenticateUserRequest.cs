using FluentValidation;

namespace ReadingIsGood.Domain.Requests
{
    public class AuthenticateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(p => p.Username)
              .NotEmpty()
              .WithMessage("Username is required.");

            RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
        }
    }
}