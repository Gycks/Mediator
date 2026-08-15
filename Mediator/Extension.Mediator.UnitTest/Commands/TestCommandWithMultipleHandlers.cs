using GycksLab.Mediator.Abstractions.Contracts;

namespace GycksLab.Mediator.UnitTest.Commands;

public record TestCommandWithMultipleHandlers(string Name) : ICommand<string>;