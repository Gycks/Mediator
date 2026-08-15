using GycksLab.Mediator.Abstractions.Contracts;
using GycksLab.Mediator.UnitTest.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GycksLab.Mediator.UnitTest;

public class TestServiceRegistrations
{
    private readonly IMediator _mediator;
    
    public TestServiceRegistrations()
    { 
        var services = new ServiceCollection();
        services.AddMediator();
        services.BuildServiceProvider();
        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task TestCommandDispatch_Fail_WithAmbiguousHandlerImplementations()
    {
        var commandWithMultipleHandlersAndNoReturnType = new TestCommandWithMultipleHandlersAndNoReturnType("test");
        var commandWithMultipleHandlers = new TestCommandWithMultipleHandlers("test");
        
        try
        {
            await _mediator.Send(commandWithMultipleHandlersAndNoReturnType, CancellationToken.None);
            Assert.False(true);
        }
        catch (InvalidOperationException)
        {
            Assert.True(true);
        }
        catch (Exception)
        {
            Assert.False(true);
        }
        
        try
        {
            await _mediator.Send(commandWithMultipleHandlers, CancellationToken.None);
            Assert.False(true);
        }
        catch (InvalidOperationException)
        {
            Assert.True(true);
        }
        catch (Exception)
        {
            Assert.False(true);
        }
        
    }

    [Fact]
    public async Task TestCommandDispatch_Success_WithUnambiguousHandlerImplementations()
    {
        var commandWithReturnType = new TestCommandWithReturnType("test");
        var commandWithoutReturnType = new TestCommandWithNoReturnType("test");
        
        var result = await _mediator.Send(commandWithReturnType, CancellationToken.None);
        await _mediator.Send(commandWithoutReturnType, CancellationToken.None);
        
        Assert.Equal(commandWithReturnType.Name, result);
    }

    [Fact]
    public async Task TestCommandDispatch_Fail_WithNoHandlerImplementations()
    {
        var commandWithNoHandlerAndNoReturnType = new TestCommandWithNoHandlerAndNoReturnType("test");
        var commandWithoutHandler= new TestCommandWithNoHandler("test");

        try
        {
            await _mediator.Send(commandWithoutHandler, CancellationToken.None);
            Assert.False(true);
        }
        catch (InvalidOperationException)
        {
            Assert.True(true);
        }
        catch (Exception)
        {
            Assert.False(true);
        }
        
        try
        {
            await _mediator.Send(commandWithNoHandlerAndNoReturnType, CancellationToken.None);
            Assert.False(true);
        }
        catch (InvalidOperationException)
        {
            Assert.True(true);
        }
        catch (Exception)
        {
            Assert.False(true);
        }
    }
}