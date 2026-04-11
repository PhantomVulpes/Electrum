using Vulpes.Electrum.Domain.Models;
using Vulpes.Electrum.Domain.Validation;

namespace Vulpes.Electrum.Domain.Interfaces;

public interface ISaveable<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    ValidateModel<SaveModel<TAggregateRoot>> PrepareForSave();
}
