namespace GycksLab.Mediator.Abstractions.Contracts;

public interface IMediator
{
    Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    
    Task Send(ICommand command, CancellationToken cancellationToken = default);
}