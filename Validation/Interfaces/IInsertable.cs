using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Validation.Interfaces;

public interface IInsertable
{
    ValidateModel<InsertModel> PrepareForInsert();
}
