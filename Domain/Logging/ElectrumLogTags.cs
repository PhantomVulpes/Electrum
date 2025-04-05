using System.Security.Cryptography.X509Certificates;

namespace Vulpes.Electrum.Domain.Logging;

public static partial class ElectrumLogTags
{
    public static string Success => $"[{Domain}.{nameof(Success)}]";
    public static string Error => $"[{Domain}.{nameof(Error)}]";
    public static string Warning => $"[{Domain}.{nameof(Warning)}]";
    public static string Info => $"[{Domain}.{nameof(Info)}]";

    public static string DatabaseOpened => $"[{Storage}.{nameof(DatabaseOpened)}]";
    public static string QueryReport => $"[{Storage}.{nameof(QueryReport)}]";
    public static string EntityDeleted => $"[{Storage}.{nameof(EntityDeleted)}]";
    public static string EntityInserted => $"[{Storage}.{nameof(EntityInserted)}]";
    public static string EntityUpdated => $"[{Storage}.{nameof(EntityUpdated)}]";

    private static string Prefix => $"{nameof(Electrum)}";

    /// <summary>
    /// The prefix for all log tags in the Electrum domain.
    /// </summary>
    private static string Domain => $"{Prefix}.{nameof(Domain)}";

    /// <summary>
    /// The prefix for all log tags related to storage operations in the Electrum domain.
    /// </summary>
    private static string Storage => $"{Prefix}.{nameof(Storage)}";
}
