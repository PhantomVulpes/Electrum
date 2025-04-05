using System;
using Vulpes.Electrum.Storage.Models;

namespace Vulpes.Electrum.Storage.Data;

public interface IModelRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task<TAggregateRoot> GetAsync(Guid key);
    Task SaveAsync(string editingToken, TAggregateRoot record);
    Task DeleteAsync(TAggregateRoot record);
    Task InsertAsync(TAggregateRoot record);
}
