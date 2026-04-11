namespace Vulpes.Electrum.Validation.Models;

public record ElectrumValidationError
{
    public string InvalidFieldName { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;

    /// <summary>
    /// Initializes a new IEnumerable of <see cref="ElectrumValidationError"/> to represent the beginning of a validation process. Technically not needed but made to simplify the validation process.
    /// </summary>
    /// <returns>An empty IEnumerable of <see cref="ElectrumValidationError"/></returns>
    public static IEnumerable<ElectrumValidationError> BeginValidation() => [];
}
