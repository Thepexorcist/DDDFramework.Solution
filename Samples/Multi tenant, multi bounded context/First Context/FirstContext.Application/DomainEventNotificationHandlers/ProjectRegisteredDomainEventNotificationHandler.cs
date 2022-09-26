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
    public class ProjectRegisteredDomainEventNotificationHandler : INotificationHandler<ProjectRegisteredDomainEventNotification>
    {
        #region Fields

        private readonly IEventBus _eventBus;
        private readonly ILogger<ProjectRegisteredDomainEventNotificationHandler> _logger;

        #endregion

        #region Constructors

        public ProjectRegisteredDomainEventNotificationHandler(IEventBus eventBus, ILogger<ProjectRegisteredDomainEventNotificationHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        #endregion

        public async Task Handle(ProjectRegisteredDomainEventNotification notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new ProjectRegisteredIntegrationEvent(
                notification.Id,
                notification.TenantId,
                notification.WorkspaceId,
                notification.UniqueProjectNumber,
                notification.ProjectName);

            await _eventBus.Publish(integrationEvent);
            _logger.LogInformation("Successfully published project registered integration event");
        }
    }
}
