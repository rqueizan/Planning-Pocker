using FluentValidation;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateCartaValidator : AbstractValidator<CreateCartaCommand>
    {
        public CreateCartaValidator()
        {
            RuleFor(self => self.Valor)
                .ValorInRange();
        }
    }
}
