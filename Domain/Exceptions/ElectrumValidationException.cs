using Vulpes.Electrum.Domain.Models;

namespace Vulpes.Electrum.Domain.Exceptions;

public class ElectrumValidationException : Exception
{
    public IEnumerable<ElectrumValidationError> ValidationResults { get; init; }

    public ElectrumValidationException(IEnumerable<ElectrumValidationError> validationResults)
    {
        ValidationResults = validationResults;
    }
}
