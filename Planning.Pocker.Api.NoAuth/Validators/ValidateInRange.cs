using FluentValidation;
using Planning.Pocker.Api.NoAuth.Handlers;

namespace Planning.Pocker.Api.NoAuth.Validators
{
    public class ValidateInRange : AbstractValidator<int>
    {
        private const int min = 3;
        private const int max = 10;

        public ValidateInRange()
        {
            RuleFor(valor => valor)
                .InclusiveBetween(min, max)
                .WithMessage($"Out of range, {min} <= {nameof(CreateCartaCommand.Valor)} <= {max}");
        }


    }
}
