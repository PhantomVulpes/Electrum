using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Validation.Interfaces;

public interface ISaveable
{
    ValidateModel<SaveModel> PrepareForSave();
}
