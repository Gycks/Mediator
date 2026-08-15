using GycksLab.Mediator.Abstractions.Contracts;

namespace GycksLab.Mediator.UnitTest.Commands;

public record TestCommandWithNoHandler(string Name) : ICommand<string>;