using Vulpes.Electrum.Domain.Security;

namespace Vulpes.Electrum.Domain.Querying;

public abstract class QueryHandler<TQuery, TResponse>
    where TQuery : Query<TResponse>
{
    protected abstract Task<TResponse> InternalRequestAsync(TQuery query);
    protected abstract Task<AccessResult> InternalValidateAccessAsync(TQuery query);

    /// <summary>
    /// Requests data from the provided query. Task is virtual to allow customization.
    /// </summary>
    public virtual async Task<TResponse> RequestAsync(TQuery query) => await InternalRequestAsync(query);

    public virtual async Task<AccessResult> ValidateAccessAsync(TQuery query) => await InternalValidateAccessAsync(query);
}
