using Vulpes.Electrum.Domain.Validation;

namespace Vulpes.Electrum.Domain.Exceptions;

public class ElectrumValidationException : Exception
{
    public IEnumerable<ElectrumValidationError> ValidationResults { get; init; }

    public ElectrumValidationException(IEnumerable<ElectrumValidationError> validationResults)
    {
        ValidationResults = validationResults;
    }
}
