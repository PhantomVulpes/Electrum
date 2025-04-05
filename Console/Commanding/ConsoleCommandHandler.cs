using Vulpes.Electrum.Validation.Models;

namespace Vulpes.Electrum.Console.Commanding;
public abstract class ConsoleCommandHandler
{
    public abstract string CommandDocumentation { get; }
    public abstract string CommandName { get; }

    public abstract string SuccessMessage(ConsoleCommand consoleCommand);
    public abstract Task ExecuteAsync(ConsoleCommand consoleCommand);

    public virtual IEnumerable<ElectrumValidationError> ValidateCommand(ConsoleCommand consoleCommand) => [];
}
