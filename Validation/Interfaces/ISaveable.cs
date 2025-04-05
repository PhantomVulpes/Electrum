using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Validation.Interfaces;

public interface ISaveable<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    ValidateModel<SaveModel<TAggregateRoot>> PrepareForSave();
}
