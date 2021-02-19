using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ReadingIsGood.API.Extensions
{
    public static class GraphQlRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapGraphQl<TSchema>(
           this IEndpointRouteBuilder endpoints,
           string pattern = "/graphql") where TSchema : ISchema
        {
            var pipeline = endpoints
                .CreateApplicationBuilder()
                .UseMiddleware<GraphQLHttpMiddleware<TSchema>>(new PathString())
                .Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}