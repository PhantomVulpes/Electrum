﻿using Vulpes.Electrum.Core.Domain.Security;

namespace Vulpes.Electrum.Core.Domain.ConsoleCommanding;
public abstract class ConsoleCommandHandler
{
    public abstract string CommandDocumentation { get; }
    public abstract string CommandName { get; }

    public abstract string SuccessMessage(ConsoleCommand consoleCommand);
    public abstract Task ExecuteAsync(ConsoleCommand consoleCommand);

    public virtual ElectrumValidationResult ValidateCommand(ConsoleCommand consoleCommand) => ElectrumValidationResult.Verify();
}
