using FluentValidation;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioCommand>
    {
        public CreateUsuarioValidator()
        {
            RuleFor(self => self.Nombre)
                .NotEmpty();
        }
    }
}
