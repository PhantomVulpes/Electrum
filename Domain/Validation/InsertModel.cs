using Vulpes.Electrum.Domain.Extensions;
using Vulpes.Electrum.Domain.Models;

namespace Vulpes.Electrum.Domain.Validation;

public sealed record InsertModel<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    private readonly AggregateRootValidationModel<TAggregateRoot> validatedValue;
    public TAggregateRoot Value => validatedValue.Value;

    public InsertModel(AggregateRootValidationModel<TAggregateRoot> validatedValue)
    {
        this.validatedValue = validatedValue;
    }

    public TAggregateRoot ThrowIfInvalid() => validatedValue.ThrowIfInvalid();
}