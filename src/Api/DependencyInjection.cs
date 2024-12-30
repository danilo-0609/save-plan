using Carter;
using FluentValidation;
using MediatR;
using SavePlan.API.Behaviors;
using SavePlan.API.Common;

namespace SavePlan.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;


        services.AddCarter();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Program>();

            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();

        return services;
    }
}
