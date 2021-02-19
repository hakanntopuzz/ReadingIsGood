using FluentValidation;

namespace ReadingIsGood.Domain.Requests
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; }
    }

    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(p => p.Name)
              .NotEmpty()
              .WithMessage("Name is required.");
        }
    }
}
