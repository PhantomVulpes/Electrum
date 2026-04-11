using Vulpes.Electrum.Domain.Models;

namespace Vulpes.Electrum.Domain.Interfaces;

public interface IInsertable<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    ValidateModel<InsertModel<TAggregateRoot>> PrepareForInsert();
}
