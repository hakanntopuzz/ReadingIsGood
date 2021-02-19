using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.Domain.Requests;

namespace ReadingIsGood.API
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
        {
            services
                .AddMvc()
                .AddFluentValidation();

            var modelTypes = typeof(CreateCustomerRequest).Assembly.GetTypes();
            new AssemblyScanner(modelTypes).ForEach(pair => {
                services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
            });

            return services;
        }
    }
}
