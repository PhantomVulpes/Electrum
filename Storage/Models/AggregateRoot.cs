namespace Vulpes.Electrum.Storage.Models;

public abstract record AggregateRoot
{
    public Guid Key { get; init; } = Guid.Empty;

    public string EditingToken { get; init; } = DateTime.MinValue.ToString();

    public virtual string ToLogName() => $"Key: {ToString()}";
}
