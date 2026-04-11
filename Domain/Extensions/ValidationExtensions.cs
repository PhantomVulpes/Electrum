using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Validation.Extensions;

public static class ValidationExtensions
{
    public static IEnumerable<ElectrumValidationError> Validate(this IEnumerable<ElectrumValidationError> validationResults, Func<bool> checkIsValid, string invalidFieldName, string errorMessage)
    {
        // If the check passes, we can return the existing validation results. Nothing new to add.
        if (checkIsValid())
        {
            return validationResults;
        }

        // If the check fails, add a new validation error
        var error = new ElectrumValidationError
        {
            InvalidFieldName = invalidFieldName,
            ErrorMessage = errorMessage
        };

        return validationResults.Append(error);
    }
}
