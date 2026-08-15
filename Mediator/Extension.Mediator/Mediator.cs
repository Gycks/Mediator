using GycksLab.Mediator.Abstractions.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GycksLab.Mediator;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;
    
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResult));
        var handlers = _serviceProvider.GetServices(handlerType).ToList();
        
        var handler = handlers.Single();
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for type {commandType}");
        }
        
        return await ((dynamic)handler).Handle((dynamic)command, cancellationToken);
    }

    public async Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var commandType = command.GetType();

        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        var handlers = _serviceProvider.GetServices(handlerType).ToList();
        
        var handler = handlers.Single();
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for type {commandType}");
        }
        
        await ((dynamic)handler).Handle((dynamic)command, cancellationToken);
    }
}