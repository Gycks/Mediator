using GycksLab.Mediator.Abstractions.Contracts;

namespace GycksLab.Mediator.UnitTest.Commands;

public record TestCommandWithNoReturnType(string Name) : ICommand;