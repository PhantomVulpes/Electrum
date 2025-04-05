using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Storage.Data;

public interface IModelRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task<TAggregateRoot> GetAsync(Guid key);
    Task SaveAsync(string editingToken, ValidateModel<SaveModel<TAggregateRoot>> record);
    Task DeleteAsync(TAggregateRoot record);
    Task InsertAsync(ValidateModel<InsertModel<TAggregateRoot>> record);
}
