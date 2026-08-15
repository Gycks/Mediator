using GycksLab.Mediator.Abstractions.Contracts;
using GycksLab.Mediator.UnitTest.Commands;

namespace GycksLab.Mediator.UnitTest.Handlers;

public class HandleMultipleFour : ICommandHandler<TestCommandWithMultipleHandlers, string>
{
    public async Task<string> Handle(TestCommandWithMultipleHandlers command, CancellationToken cancellationToken = default)
    {
        return command.Name;
    }
}