using Vulpes.Electrum.Domain.Monads;

namespace Vulpes.Electrum.Domain.Extensions;
public static class PerhapsExtensions
{
    public static Perhaps<TItem> FirstToPerhaps<TItem>(this IEnumerable<TItem> items) => items.Any() ? Perhaps<TItem>.ToPerhaps(items.First()) : Perhaps<TItem>.Empty;
    public static Perhaps<TItem> LastToPerhaps<TItem>(this IEnumerable<TItem> items) => items.Any() ? Perhaps<TItem>.ToPerhaps(items.Last()) : Perhaps<TItem>.Empty;
}