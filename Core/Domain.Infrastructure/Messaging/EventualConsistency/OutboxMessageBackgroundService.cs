using Domain.DomainEvents;
using Domain.Infrastructure.Messaging.EventualConsistency.Interfaces;
using Domain.Infrastructure.Messaging.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.EventualConsistency
{
    public class OutboxMessageBackgroundService<TContext> : BackgroundService where TContext : DbContext, IOutboxMessageDbContext
    {
        #region Fields

        private readonly int _pollingInterval = 0;
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxMessageBackgroundService<TContext>> _logger;

        #endregion

        #region Properties



        #endregion

        #region Constructors

        public OutboxMessageBackgroundService(ILogger<OutboxMessageBackgroundService<TContext>> logger, IMediator mediator, IServiceProvider serviceProvider, int pollingInterval)
        {
            _mediator = mediator;
            _pollingInterval = pollingInterval;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var serviceScopeFactory = _serviceProvider.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory;
                    var scope = serviceScopeFactory.CreateScope();
                    using (scope)
                    {
                        var context = scope.ServiceProvider.GetRequiredService<TContext>();
                        var outboxMessages = context.OutboxMessages.Where(x => x.ProcessedDate == null).OrderBy(x => x.OccurredOn).ToList();

                        foreach (var outboxMessage in outboxMessages)
                        {
                            var domainEventNotification = JsonConvert.DeserializeObject(
                                    outboxMessage.Data,
                                    Type.GetType(outboxMessage.Type)) as IDomainEventNotification<DomainEventBase>;

                            // Make sure identifier is the identifier from the database table and not some random generated GUID.
                            domainEventNotification.Id = outboxMessage.Id;

                            // This will always publish the notification to be handled with external communication.
                            // If this fails the update for handling the outbox message till not be updated and this will re-run until
                            // message is successfully handled.
                            await _mediator.Publish(domainEventNotification);

                            outboxMessage.ProcessedDate = DateTime.Now;
                            context.Update(outboxMessage);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error trying to process outbox messages because: {0}", ex.Message);
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }
    }
}
