using FluentValidation;

namespace ReadingIsGood.Domain.Requests
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public int BookId { get; set; }
    }

    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(p => p.CustomerId)
              .NotNull()
              .WithMessage("CustomerId is required.");

            RuleFor(p => p.BookId)
             .NotNull()
             .WithMessage("BookId is required.");
        }
    }
}