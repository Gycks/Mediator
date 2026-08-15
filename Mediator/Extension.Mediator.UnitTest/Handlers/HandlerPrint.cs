using GycksLab.Mediator.Abstractions.Contracts;
using GycksLab.Mediator.UnitTest.Commands;

namespace GycksLab.Mediator.UnitTest.Handlers;

public class HandlerPrint : ICommandHandler<TestCommandWithNoReturnType>
{
    public async Task Handle(TestCommandWithNoReturnType command, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(command.Name);
    }
}