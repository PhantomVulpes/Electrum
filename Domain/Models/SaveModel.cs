namespace Vulpes.Electrum.Domain.Models;

public sealed record SaveModel<TAggregateRoot>(TAggregateRoot Entity) where TAggregateRoot : AggregateRoot;