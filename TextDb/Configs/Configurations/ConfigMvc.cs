using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TextDb.Configs.Configurations
{
    public static class ConfigMvc
    {
        public static void AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                ;
        }
        
        public static void UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}