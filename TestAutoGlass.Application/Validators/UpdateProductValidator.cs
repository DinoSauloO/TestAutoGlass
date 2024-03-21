using FluentValidation;
using TestAutoGlass.Domain.Requests.Update;

namespace TestAutoGlass.Application.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
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
