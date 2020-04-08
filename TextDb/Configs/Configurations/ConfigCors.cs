using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TextDb.Configs.Configurations
{
    public static class ConfigCors
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyCors",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
        
        public static void UseCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors("MyCors"); 
        }
    }
}