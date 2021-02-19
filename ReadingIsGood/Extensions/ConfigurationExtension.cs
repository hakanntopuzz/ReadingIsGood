using GraphQL.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.API.Profiler;
using ReadingIsGood.Data;

namespace ReadingIsGood.API
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database

            services.AddDbContext<ReadingDBContext>(_ => _.UseSqlServer(configuration["ConnectionStrings:ReadingDbConnection"]));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(Mapper));
            #endregion

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            #region GraphQL

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true;
            })
            .AddGraphTypes(ServiceLifetime.Scoped);

            #endregion

            return services;
        }
    }
}