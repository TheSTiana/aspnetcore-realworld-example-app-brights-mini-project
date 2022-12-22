using System.Reflection;
using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(QueryBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}