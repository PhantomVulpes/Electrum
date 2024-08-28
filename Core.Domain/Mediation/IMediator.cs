using Vulpes.Electrum.Core.Domain.Commanding;
using Vulpes.Electrum.Core.Domain.Querying;

namespace Vulpes.Electrum.Core.Domain.Mediation;
public interface IMediator
{
    Task<TResponse> RequestResponseAsync<TQuery, TResponse>(TQuery query)
        where TQuery : Query;

    Task ExecuteCommandAsync<TCommand>(TCommand command)
        where TCommand : Command;
    Task<bool> EvaluateAccessAsync<TCommand>(TCommand command)
        where TCommand : Command;
}
