using Vulpes.Electrum.Domain.Models;
using Vulpes.Electrum.Domain.Validation;

namespace Vulpes.Electrum.Domain.Interfaces;

public interface IInsertable<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    ValidateModel<InsertModel<TAggregateRoot>> PrepareForInsert();
}
