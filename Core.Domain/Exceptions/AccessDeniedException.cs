using Vulpes.Electrum.Core.Domain.Security;

namespace Vulpes.Electrum.Core.Domain.Exceptions;
public class AccessDeniedException : Exception
{
    public AccessResult AccessResult { get; init; } = AccessResult.Empty;

    public AccessDeniedException(AccessResult accessResult) : base(accessResult.Message)
    {
        AccessResult = accessResult;
    }
}
