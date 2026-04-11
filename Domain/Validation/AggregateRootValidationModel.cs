using Vulpes.Electrum.Domain.Models;

namespace Vulpes.Electrum.Domain.Validation;

public record AggregateRootValidationModel<TAggregateRoot> : IValidationModel<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    public TAggregateRoot Value { get; init; }
    public ValidationBuilder ValidationBuilder { get; init; }

    public AggregateRootValidationModel(TAggregateRoot value, ValidationBuilder validationBuilder)
    {
        Value = value;
        ValidationBuilder = validationBuilder;
    }
}