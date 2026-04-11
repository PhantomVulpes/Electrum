using Vulpes.Electrum.Domain.Extensions;
using Vulpes.Electrum.Domain.Models;

namespace Vulpes.Electrum.Domain.Validation;

public sealed record SaveModel<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    private readonly AggregateRootValidationModel<TAggregateRoot> validatedValue;

    public TAggregateRoot Value => validatedValue.Value;
    public string PreviousEditingToken { get; init; }

    public SaveModel(AggregateRootValidationModel<TAggregateRoot> validatedValue, string previousEditingToken)
    {
        this.validatedValue = validatedValue;
        PreviousEditingToken = previousEditingToken;
    }

    public TAggregateRoot ThrowIfInvalid() => validatedValue.ThrowIfInvalid();
}