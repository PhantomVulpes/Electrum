namespace Vulpes.Electrum.Core.Domain.Exceptions;
public class RelicScandalException : Exception
{
    public Type RequestedType { get; init; }

    public RelicScandalException(Type requestedType)
        : base($"The relic of type {requestedType.Name} is worthless.")
    {
        RequestedType = requestedType;
    }
    public RelicScandalException(string message, Type requestedType)
        : base(message)
    {
        RequestedType = requestedType;
    }
}
