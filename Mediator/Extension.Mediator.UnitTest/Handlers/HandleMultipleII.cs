using GycksLab.Mediator.Abstractions.Contracts;
using GycksLab.Mediator.UnitTest.Commands;

namespace GycksLab.Mediator.UnitTest.Handlers;

public class HandleMultipleTwo : ICommandHandler<TestCommandWithMultipleHandlersAndNoReturnType>
{
    public async Task Handle(TestCommandWithMultipleHandlersAndNoReturnType command, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(command.Name);
    }
}