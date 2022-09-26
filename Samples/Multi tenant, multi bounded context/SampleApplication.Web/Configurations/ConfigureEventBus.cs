using Autofac;
using EventBus.InMemory;
using EventBus.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApplication.Web.Configurations
{
    /// <summary>
    /// Configuration of the settings used in the application.
    /// </summary>
    public static class ConfigureEventBus
    {
        public static IServiceCollection AddAndConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus>(e =>
                new InMemoryEventBusClient(
                    e.GetService<ILifetimeScope>()));

            return services;
        }
    }
}
