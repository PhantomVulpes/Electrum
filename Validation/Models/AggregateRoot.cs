using Vulpes.Electrum.Validation.Interfaces;

namespace Vulpes.Electrum.Validation.Models;

public abstract record AggregateRoot : IInsertable, ISaveable
{
    public Guid Key { get; init; } = Guid.Empty;

    public string EditingToken { get; init; } = DateTime.MinValue.ToString();

    public virtual string ToLogName() => $"Key: {ToString()}";

    public abstract ValidateModel<InsertModel> PrepareForInsert();
    public abstract ValidateModel<SaveModel> PrepareForSave();
}
