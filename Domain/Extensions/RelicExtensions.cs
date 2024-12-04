﻿using Vulpes.Electrum.Domain.Monads;

namespace Vulpes.Electrum.Domain.Extensions;
public static class RelicExtensions
{
    public static Relic<TDiscovery> ConcealFirst<TDiscovery>(this IEnumerable<TDiscovery> items) => items.Any() ? Relic<TDiscovery>.Conceal(items.First()) : Relic<TDiscovery>.Empty;
    public static Relic<TDiscovery> ConcealLast<TDiscovery>(this IEnumerable<TDiscovery> items) => items.Any() ? Relic<TDiscovery>.Conceal(items.Last()) : Relic<TDiscovery>.Empty;
}