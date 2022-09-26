using Domain.Infrastructure.Messaging.EventualConsistency;
using FirstContext.Infrastructure.Persistance;
using MediatR;

namespace SampleApplication.Web.Configurations
{
    public static class ConfigureFirstContextEventualConsistency
    {
        public static IServiceCollection AddAndConfigureFirstContextEventualConsistency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<OutboxMessageBackgroundService<FirstContextDbContext>>(x =>
                new OutboxMessageBackgroundService<FirstContextDbContext>(x.GetRequiredService<ILogger<OutboxMessageBackgroundService<FirstContextDbContext>>>(),
                    x.GetRequiredService<IMediator>(),
                    x.GetRequiredService<IServiceProvider>(),
                    5000));

            services.AddHostedService(x => x.GetService<OutboxMessageBackgroundService<FirstContextDbContext>>() as OutboxMessageBackgroundService<FirstContextDbContext>);

            return services;
        }
    }
}
