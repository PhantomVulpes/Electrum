namespace Vulpes.Electrum.Domain.Validation;

public class ValidationBuilder
{
    public List<ElectrumValidationError> Errors { get; } = [];

    public ValidationBuilder InvalidIf(Func<bool> precondition, Func<ElectrumValidationError> errorBuilder)
    {
        if (precondition())
        {
            Errors.Add(errorBuilder());
        }

        return this;
    }

    public ValidationBuilder AddError<TSubject>(TSubject validating, string code, string message)
        where TSubject : notnull
    {
        Errors.Add(new(code, message, validating.GetType()));
        return this;
    }
}