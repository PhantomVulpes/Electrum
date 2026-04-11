using Vulpes.Electrum.Domain.Exceptions;
using Vulpes.Electrum.Domain.Validation;

namespace Vulpes.Electrum.Domain.Extensions;

public static class ValidationExtensions
{
    public static bool IsValid(this ValidationBuilder builder) => builder.Errors.Count == 0;

    public static ValidationBuilder ThrowIfInvalid(this ValidationBuilder builder)
    {
        if (!builder.IsValid())
        {
            throw new ElectrumValidationException(builder.Errors);
        }

        return builder;
    }

    public static TSubject ThrowIfInvalid<TSubject>(this IValidationModel<TSubject> validationModel)
    {
        validationModel.ThrowIfInvalid();
        return validationModel.Value;
    }
}
