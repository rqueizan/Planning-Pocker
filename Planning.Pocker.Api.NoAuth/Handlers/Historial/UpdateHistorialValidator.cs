using FluentValidation;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateHistorialValidator : AbstractValidator<UpdateHistorialCommand>
    {
        public UpdateHistorialValidator()
        {
            RuleFor(self => self.Descripcion)
                .NotEmpty();
        }
    }
}
