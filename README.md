# GycksLab.Mediator

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.1-blue.svg)](https://dotnet.microsoft.com/)

A minimal in-process mediator for .NET. It dispatches commands to handlers resolved from `Microsoft.Extensions.DependencyInjection`, with no message-pipeline, no notifications, no reflection magic beyond assembly scanning at startup.

## Features

- **Two command shapes**: `ICommand` for fire-and-forget, `ICommand<TResult>` for commands that return a value
- **Convention-based registration**: handlers are discovered and registered via [Scrutor](https://github.com/khellang/Scrutor) assembly scanning — no manual `services.AddScoped<...>()` per handler
- **No external message pipeline**: `IMediator` is a thin dispatcher, not a behavior/pipeline framework. Feel free to add cross-cutting concerns (e.g logging, validation) as decorators around your handlers if you need them

## Installation

### NuGet

```
dotnet add package GycksLab.Mediator
```

## Quick Start

**1. Define a command**

```csharp
using GycksLab.Mediator.Abstractions.Contracts;

public record CreateConnection(string Host, int Port) : ICommand<Guid>;
```

**2. Implement its handler**

```csharp
using GycksLab.Mediator.Abstractions.Contracts;

public class CreateConnectionHandler : ICommandHandler<CreateConnection, Guid>
{
    public async Task<Guid> Handle(CreateConnection command, CancellationToken cancellationToken = default)
    {
        // ... application logic ...
        return Guid.NewGuid();
    }
}
```

**3. Register the mediator**

```csharp
using GycksLab.Mediator;

// Scans the given assembly (or assemblies) for ICommandHandler implementations
// and registers IMediator itself.
builder.Services.AddMediator();
```

**4. Dispatch from a controller (or any consumer holding `IMediator`)**

```csharp
[ApiController]
[Route("connections")]
public class ConnectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConnectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateConnection command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return Ok(id);
    }
}
```

Commands without a return value work the same way, just implement `ICommand` and `ICommandHandler<TCommand>` (`Task Handle(...)` instead of `Task<TResult> Handle(...)`), and call `await _mediator.Send(command, cancellationToken);`.

## API Reference

- `ICommand` / `ICommand<TResult>` — marker interfaces for a dispatchable command, with or without a result
- `ICommandHandler<TCommand>` / `ICommandHandler<TCommand, TResult>` — implement one per command to handle it
- `IMediator` — resolved via DI. Exposes `Send(ICommand, ...)` and `Send<TResult>(ICommand<TResult>, ...)`

## Design Notes

- Exactly one handler must be registered per command type. Zero or more than one registered handlers throws `InvalidOperationException` at dispatch time. This is intentional.
- There's no built-in pipeline/behavior mechanism (validation, logging, retries, etc.). If you need cross-cutting concerns, wrap the specific handler with a decorator — [Scrutor supports `Decorate<TService, TDecorator>()`](https://github.com/khellang/Scrutor#decoration) for this.

## License

MIT
