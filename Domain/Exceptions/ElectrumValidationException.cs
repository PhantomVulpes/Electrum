using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Storage.Exceptions;

public class ElectrumValidationException : Exception
{
    public IEnumerable<ElectrumValidationError> ValidationResults { get; init; }

    public ElectrumValidationException(IEnumerable<ElectrumValidationError> validationResults)
    {
        ValidationResults = validationResults;
    }
}
