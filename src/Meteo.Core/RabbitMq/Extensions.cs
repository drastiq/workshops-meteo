using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Meteo.Core.Commands;
using Meteo.Core.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Exceptions;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;

namespace Meteo.Core.RabbitMq
{
    public static class Extensions
    {
        public static IRabbitMqBuilder UseRabbitMq(this IApplicationBuilder app)
            => new RabbitMqBuilder(app);

        public static void AddRabbitMq(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var section = configuration.GetSection("rabbitmq");
                var rawRabbitConfiguration = new RawRabbitConfiguration();
                section.Bind(rawRabbitConfiguration);

                return rawRabbitConfiguration;
            }).SingleInstance();

            var assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerDependency();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();
            builder.RegisterType<BusPublisher>().As<IBusPublisher>()
                .InstancePerDependency();

            ConfigureRabbitMq(builder);
        }

        private static void ConfigureRabbitMq(ContainerBuilder builder)
        {
            builder.Register<IInstanceFactory>(context =>
                RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions
                {
                    DependencyInjection = ioc => ioc.AddSingleton(context.Resolve<RawRabbitConfiguration>())
                })).SingleInstance();
            builder.Register(context => context.Resolve<IInstanceFactory>().Create());
        }

        public interface IRabbitMqBuilder
        {
            IRabbitMqBuilder SubscribeCommand<T>() where T : ICommand;
            IRabbitMqBuilder SubscribeEvent<T>() where T : IEvent;
        }

        public class RabbitMqBuilder : IRabbitMqBuilder
        {
            private readonly IBusClient _busClient;
            private readonly IServiceProvider _serviceProvider;

            public RabbitMqBuilder(IApplicationBuilder app)
            {
                _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
                _busClient = _serviceProvider.GetService<IBusClient>();
            }

            public IRabbitMqBuilder SubscribeCommand<T>() where T : ICommand
            {
                _busClient.SubscribeAsync<T>(msg => _serviceProvider
                    .GetService<ICommandHandler<T>>().HandleAsync(msg),
                        ctx => ctx.UseSubscribeConfiguration(cfg => 
                            cfg.FromDeclaredQueue(q => q.WithName(GetExchangeName<T>()))));

                return this;
            }

            public IRabbitMqBuilder SubscribeEvent<T>() where T : IEvent
            {
                _busClient.SubscribeAsync<T>(msg => _serviceProvider
                    .GetService<IEventHandler<T>>().HandleAsync(msg),
                        ctx => ctx.UseSubscribeConfiguration(cfg => 
                            cfg.FromDeclaredQueue(q => q.WithName(GetExchangeName<T>()))));

                return this;
            }
        }

        private static string GetExchangeName<T>(string name = null)
            => string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}"
                : $"{name}/{typeof(T).Name}";
    }
}