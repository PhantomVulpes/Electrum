using Vulpes.Electrum.Core.Domain.Security;

namespace Vulpes.Electrum.Core.Domain.Exceptions;
public class ElectrumValidationException : Exception
{
    public ElectrumValidationException(ElectrumValidationResult electrumValidationResult) : base(string.Join(", ", electrumValidationResult.Messages))
    { }
}
