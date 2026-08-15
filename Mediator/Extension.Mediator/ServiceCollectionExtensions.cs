using System.Reflection;
using GycksLab.Mediator.Abstractions.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GycksLab.Mediator;

public static class ServiceCollectionExtensions
{
    public static void AddMediator(this IServiceCollection serviceCollection)
    {
        var assemblies = GetCandidateAssemblies();
        
        serviceCollection.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => 
                c.AssignableTo(typeof(ICommandHandler<,>))
            )
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        serviceCollection.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => 
                c.AssignableTo(typeof(ICommandHandler<>))
            )
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        

        serviceCollection.AddScoped<IMediator, Mediator>();
    }
    
    private static IEnumerable<Assembly> GetCandidateAssemblies()
    {
        var mediatorAssembly = typeof(ICommand<>).Assembly;

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a =>
                !a.IsDynamic &&
                a.GetReferencedAssemblies().Any(r => r.FullName == mediatorAssembly.FullName)
            )
            .Concat(new[] { mediatorAssembly })
            .Distinct();
    }
}