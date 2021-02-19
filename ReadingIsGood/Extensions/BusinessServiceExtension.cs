using GraphQL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.API.GraphQL;
using ReadingIsGood.API.GraphQL.Types;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Data.Repositories;

namespace ReadingIsGood.API
{
    public static class BusinessServiceExtension
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            services.Scan(x =>
            {
                x.FromAssembliesOf(typeof(ICustomerService))
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<OrderType>();
            services.AddSingleton<CustomerType>();

            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ReadingSchema>();
            services.AddScoped<ReadingQuery>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDependencyResolver>(x =>
             new FuncDependencyResolver(x.GetRequiredService));

            return services;
        }
    }
}