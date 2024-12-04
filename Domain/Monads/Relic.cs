using Vulpes.Electrum.Core.Domain.Exceptions;

namespace Vulpes.Electrum.Domain.Monads;
public class Relic<TDiscovery>
{
    public static Relic<TDiscovery> Empty => new(default!, true);
    public static Relic<TDiscovery> Conceal(TDiscovery discovery)
    {
        // If the type is a string and it is an empty string, return an empty relic.
        if (typeof(TDiscovery) == typeof(string) && string.IsNullOrWhiteSpace(discovery as string))
        {
            return Empty;
        }

        return new Relic<TDiscovery>(discovery);
    }

    private readonly TDiscovery discovery;

    public bool IsWorthless { get; init; } = true;

    private Relic(TDiscovery discovery, bool isWorthless)
    {
        this.discovery = discovery;
        IsWorthless = isWorthless;
    }

    public Relic(TDiscovery discovery)
    {
        this.discovery = discovery;
        IsWorthless = discovery != null;
    }

    public TDiscovery Reveal() => discovery;
    public TDiscovery RevealOrSubstitute(TDiscovery contingency) => discovery ?? contingency;
    public TDiscovery RevealOrHoax(Exception exception) => discovery ?? throw exception;
    public TDiscovery RevealOrHoax(string message) => RevealOrHoax(new RelicScandalException(message, typeof(TDiscovery)));
    public TDiscovery RevealOrHoax() => RevealOrHoax(new RelicScandalException(typeof(TDiscovery)));
}
