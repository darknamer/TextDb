using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TextDb.Configs.Configurations
{
    public static class ConfigAutomapper
    {
        public static void AddAutomapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}