using Vulpes.Electrum.Domain.Models;
using Vulpes.Electrum.Domain.Validation;

namespace Vulpes.Electrum.Domain.Data;

public interface IModelRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task<TAggregateRoot> GetAsync(Guid key);
    Task SaveAsync(SaveModel<TAggregateRoot> saveRecord);
    Task DeleteAsync(TAggregateRoot record);
    Task InsertAsync(InsertModel<TAggregateRoot> insertRecord);
}
