using Domain.Infrastructure.Messaging;
using Domain.Infrastructure.Messaging.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.DomainEventNotifications
{
    public class ProjectPublishedDomainEventNotification : DomainEventNotificationBase<ProjectPublished>
    {
        #region Properties

        public int TenantId { get; }
        public int WorkspaceId { get; }
        public string UniqueProjectNumber { get; }

        #endregion

        public ProjectPublishedDomainEventNotification(ProjectPublished domainEvent) : base(domainEvent)
        {
            TenantId = domainEvent.TenantId.Value;
            WorkspaceId = domainEvent.WorkspaceId.Value;
            UniqueProjectNumber = domainEvent.UniqueProjectNumber;
        }

        [JsonConstructor]
        public ProjectPublishedDomainEventNotification(int tenantId, int workspaceId, string uniqueProjectNumber) : base(null)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
        }
    }
}
