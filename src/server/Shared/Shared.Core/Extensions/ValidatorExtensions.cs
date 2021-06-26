using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentValidation;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeJson<T>(this IRuleBuilderInitial<T, string> ruleBuilder, IJsonSerializer jsonSerializer)
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(value =>
                {
                    var isJson = true;
                    value = value.Trim();
                    try
                    {
                        jsonSerializer.Deserialize<object>(value);
                    }
                    catch
                    {
                        isJson = false;
                    }
                    isJson = isJson && value.StartsWith("{") && value.EndsWith("}")
                             || value.StartsWith("[") && value.EndsWith("]");

                    return isJson;
                })
                .WithMessage("'{PropertyName}' must be a valid JSON string.");
    }
}