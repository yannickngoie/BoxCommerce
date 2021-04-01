using FluentValidation;

namespace Order.Application.Features.Orders.Commands.CheckOutOrder
{
    class CheckoutOrderCommandValidator: AbstractValidator<CheckoutOrderCommand>
    {

        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.IDNumber)
                .NotEmpty().WithMessage("{IDNumber} is required.")
                .NotNull();
            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{EmailAddress} is required.");
                
        }
    }
}

