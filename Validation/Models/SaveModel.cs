namespace Vulpes.Electrum.Validation.Models;

public sealed record SaveModel<TAggregateRoot>(TAggregateRoot Entity) where TAggregateRoot : AggregateRoot;