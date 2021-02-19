using FluentValidation;

namespace ReadingIsGood.Model.Requests
{
    public class UpdateStockRequest
    {
        public int BookId { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequest>
    {
        public UpdateStockRequestValidator()
        {
            RuleFor(p => p.BookId)
              .NotNull()
              .WithMessage("BookId is required.")
              .GreaterThan(0)
              .WithMessage("BookId must be greater than zero.");

            RuleFor(p => p.Stock)
             .NotNull()
             .WithMessage("Stock is required.")
             .GreaterThan(0)
             .WithMessage("Stock must be greater than zero.");
        }
    }
}
