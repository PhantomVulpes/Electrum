namespace Vulpes.Electrum.Domain.Models;

public sealed record InsertModel<TAggregateRoot>(TAggregateRoot Entity) where TAggregateRoot : AggregateRoot;