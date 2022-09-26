using Domain.Infrastructure.Messaging;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.DomainEvents;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.DomainEventHandlers
{
    public class TenanDeactivatedDomainEventHandler : INotificationHandler<WrappedDomainEvent<TenantDeactivated>>
    {
        #region Fields

        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ILogger<TenanDeactivatedDomainEventHandler> _logger;

        #endregion

        #region Constructors

        public TenanDeactivatedDomainEventHandler(IWorkspaceRepository workspaceRepository, ILogger<TenanDeactivatedDomainEventHandler> logger)
        {
            _workspaceRepository = workspaceRepository;
            _logger = logger;
        }

        #endregion

        public async Task Handle(WrappedDomainEvent<TenantDeactivated> notification, CancellationToken cancellationToken)
        {
            var workspaces = await _workspaceRepository.GetAllAsync(notification.DomainEvent.Tenant.Id);

            if (workspaces.Count == 0)
            {
                return;
            }

            foreach (var workspace in workspaces)
            {
                workspace.Deactivate();
                await _workspaceRepository.UnitOfWork.SaveAggregateRootAsync(workspace, cancellationToken);
            }

            _logger.LogInformation("Successfully deactivated {0} workspaces", workspaces.Count);
        }
    }
}
