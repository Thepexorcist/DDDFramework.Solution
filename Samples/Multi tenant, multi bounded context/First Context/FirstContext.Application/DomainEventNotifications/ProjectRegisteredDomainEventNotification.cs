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
    public class ProjectRegisteredDomainEventNotification : DomainEventNotificationBase<ProjectRegistered>
    {
        #region Properties

        public int TenantId { get; }
        public int WorkspaceId { get; }
        public string UniqueProjectNumber { get; }
        public string ProjectName { get; }

        #endregion

        public ProjectRegisteredDomainEventNotification(ProjectRegistered domainEvent) : base(domainEvent)
        {
            TenantId = domainEvent.TenantId.Value;
            WorkspaceId = domainEvent.WorkspaceId.Value;
            UniqueProjectNumber = domainEvent.UniqueProjectNumber;
            ProjectName = domainEvent.ProjectName;
        }

        [JsonConstructor]
        public ProjectRegisteredDomainEventNotification(int tenantId, int workspaceId, string uniqueProjectNumber, string projectName) : base(null)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
            ProjectName = projectName;
        }
    }
}
