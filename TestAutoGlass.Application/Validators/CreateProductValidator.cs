using FluentValidation;
using TestAutoGlass.Domain.Requests.Create;

namespace TestAutoGlass.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.ManufacturingDate)
                .NotEmpty()
                .LessThan(x => x.ExpirationDate)
                .WithMessage("Manufacturing Date cannot be greater than or equal to the Expiry Date");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .WithMessage("ExpirationDate is required");
        }
    }
}
