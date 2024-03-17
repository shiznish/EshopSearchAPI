using Application.Core.Abstractions.Behaviors;
using Application.Core.Abstractions.Sort.Relevance.Product;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(assembly);



        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        services.AddValidatorsFromAssembly(assembly,
            includeInternalTypes: true);

        services.AddTransient<IProductSearchService, ProductSearchService>();

        return services;
    }
}
