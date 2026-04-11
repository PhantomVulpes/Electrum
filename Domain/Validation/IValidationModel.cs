using Vulpes.Electrum.Domain.Extensions;

namespace Vulpes.Electrum.Domain.Validation;

public interface IValidationModel<out TSubject>
{
    TSubject Value { get; }
    ValidationBuilder ValidationBuilder { get; }

    public void ThrowIfInvalid() => ValidationBuilder.ThrowIfInvalid();
}