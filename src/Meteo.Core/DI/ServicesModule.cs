using Autofac;
using Meteo.Core.EF;
using Meteo.Core.Repositories;

namespace Meteo.Core.DI
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ServicesModule).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterType<SqlCityRepository>()
                .As<ICityRepository>()
                .InstancePerLifetimeScope();
        }
    }
}