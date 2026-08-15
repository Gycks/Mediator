using GycksLab.Mediator.Abstractions.Contracts;
using GycksLab.Mediator.UnitTest.Commands;

namespace GycksLab.Mediator.UnitTest.Handlers;

public class HandleReturnEntries : ICommandHandler<TestCommandWithReturnType, string>
{
    public async Task<string> Handle(TestCommandWithReturnType command, CancellationToken cancellationToken = default)
    {
        return command.Name;
    }
}