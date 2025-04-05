namespace Vulpes.Electrum.Validation.Models;

public sealed record InsertModel<TAggregateRoot>(TAggregateRoot Entity) where TAggregateRoot : AggregateRoot;