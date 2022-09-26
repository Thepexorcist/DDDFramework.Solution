using EventBus.Interfaces;
using FirstContext.Application.DomainEventNotifications;
using FirstContext.Application.IntegrationEvents.Outgoing;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.DomainEventNotificationHandlers
{
    public class ProjectPublishedDomainEventNotificationHandler : INotificationHandler<ProjectPublishedDomainEventNotification>
    {
        #region Fields

        private readonly IEventBus _eventBus;
        private readonly ILogger<ProjectRegisteredDomainEventNotificationHandler> _logger;

        #endregion

        #region Constructors

        public ProjectPublishedDomainEventNotificationHandler(IEventBus eventBus, ILogger<ProjectRegisteredDomainEventNotificationHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        #endregion

        public async Task Handle(ProjectPublishedDomainEventNotification notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new ProjectPublishedIntegrationEvent(
                notification.Id,
                notification.TenantId,
                notification.WorkspaceId,
                notification.UniqueProjectNumber);

            await _eventBus.Publish(integrationEvent);
            _logger.LogInformation("Successfully published project published integration event");
        }
    }
}
