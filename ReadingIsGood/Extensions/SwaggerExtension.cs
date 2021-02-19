using Microsoft.AspNetCore.Builder;

namespace ReadingIsGood.API
{
    public static class SwaggerExtension
    {
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "ReadingIsGood v1");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}