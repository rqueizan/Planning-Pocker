using FluentValidation;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateHistorialValidator : AbstractValidator<CreateHistorialCommand>
    {
        public CreateHistorialValidator()
        {
            RuleFor(self => self.Descripcion)
                .NotEmpty();
        }
    }
}
