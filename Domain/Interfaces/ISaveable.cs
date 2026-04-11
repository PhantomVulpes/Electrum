using Vulpes.Electrum.Domain.Models;

namespace Vulpes.Electrum.Domain.Interfaces;

public interface ISaveable<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    ValidateModel<SaveModel<TAggregateRoot>> PrepareForSave();
}
