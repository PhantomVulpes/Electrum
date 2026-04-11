using Vulpes.Electrum.Domain.Extensions;

namespace Vulpes.Electrum.Domain.Validation;

public class ElectrumValidationError
{
    public string Code { get; }
    public string Message { get; }
    public Type? EntityType { get; }
    public Dictionary<string, string> Data { get; }

    public ElectrumValidationError(string code, string message, Type? entityType = default, IReadOnlyDictionary<string, string>? data = default)
    {
        Code = code;
        Message = message;
        EntityType = entityType;
        Data = data?.CloneDictionary() ?? [];
    }
}
