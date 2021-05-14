using FluentValidation;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateCartaValidator : AbstractValidator<UpdateCartaCommand>
    {
        public UpdateCartaValidator()
        {
            RuleFor(self => self.Valor)
                .ValorInRange();
        }
    }
}
