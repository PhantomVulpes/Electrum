namespace Vulpes.Electrum.Domain.Exceptions;

public class ModificationException : Exception
{
    public Dictionary<string, string> AdditionalData { get; init; } = [];

    public ModificationException(string message, Dictionary<string, string> additionalData)
        : base(message)
    {
        AdditionalData = additionalData;
    }
}
