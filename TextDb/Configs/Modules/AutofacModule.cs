using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TextDb.Configs.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const StringComparison stringCompare = StringComparison.CurrentCultureIgnoreCase;
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Service", stringCompare) ||
                            x.Name.EndsWith("Repository", stringCompare))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            // builder.Register(context =>
            // {
            //     var configuration = context.Resolve<IConfiguration>();
            //     var options = configuration.GetOptions<JwtOption>("Jwt");
            //     return options;
            // }).SingleInstance();
            //
            // builder.Register(context =>
            // {
            //     var configuration = context.Resolve<IConfiguration>();
            //     var options = configuration.GetOptions<SqlServerOption>("Database:SqlServer");
            //     return options;
            // }).SingleInstance();
            //
            // builder.Register(context =>
            // {
            //     var configuration = context.Resolve<IConfiguration>();
            //     var options = configuration.GetOptions<WebServiceOption>("WebService");
            //     return options;
            // }).SingleInstance();
        }

    }
}