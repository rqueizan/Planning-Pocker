using FluentValidation;
using Planning.Pocker.Api.NoAuth.Validators;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, int> ValorInRange<T>(
            this IRuleBuilder<T, int> ruleBuilder) => ruleBuilder?.SetValidator(new ValidateInRange());
    }
}
