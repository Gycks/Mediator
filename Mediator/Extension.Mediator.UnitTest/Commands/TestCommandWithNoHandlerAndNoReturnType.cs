using GycksLab.Mediator.Abstractions.Contracts;

namespace GycksLab.Mediator.UnitTest.Commands;

public record TestCommandWithNoHandlerAndNoReturnType(string Name) : ICommand;