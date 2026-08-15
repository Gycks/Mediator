using GycksLab.Mediator.Abstractions.Contracts;

namespace GycksLab.Mediator.UnitTest.Commands;

public record TestCommandWithMultipleHandlersAndNoReturnType(string Name) : ICommand;