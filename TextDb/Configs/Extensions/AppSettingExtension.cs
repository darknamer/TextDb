using Microsoft.Extensions.Configuration;

namespace TextDb.Configs.Extensions
{
    public static class AppSettingExtension
    {
        public static T GetOptions<T>(this IConfiguration configuration, string section) where T : new()
        {
            var model = new T();
            configuration.GetSection(section).Bind(model);
            return model;
        }
    }
}