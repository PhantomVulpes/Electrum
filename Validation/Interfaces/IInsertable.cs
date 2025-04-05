using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Validation.Interfaces;

public interface IInsertable<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    ValidateModel<InsertModel<TAggregateRoot>> PrepareForInsert();
}
