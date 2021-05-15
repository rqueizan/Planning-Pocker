using FluentValidation;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateUsuarioValidator : AbstractValidator<UpdateUsuarioCommand>
    {
        public UpdateUsuarioValidator()
        {
            RuleFor(self => self.Nombre)
                .NotEmpty();
        }
    }
}
