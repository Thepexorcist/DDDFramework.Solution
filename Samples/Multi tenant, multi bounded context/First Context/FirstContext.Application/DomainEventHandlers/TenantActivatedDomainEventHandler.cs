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
    public class TenantActivatedDomainEventHandler : INotificationHandler<WrappedDomainEvent<TenantActivated>>
    {
        #region Fields

        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ILogger<TenantActivatedDomainEventHandler> _logger;

        #endregion

        #region Constructors

        public TenantActivatedDomainEventHandler(IWorkspaceRepository workspaceRepository, ILogger<TenantActivatedDomainEventHandler> logger)
        {
            _workspaceRepository = workspaceRepository;
            _logger = logger;
        }

        #endregion

        public async Task Handle(WrappedDomainEvent<TenantActivated> notification, CancellationToken cancellationToken)
        {
            var workspaces = await _workspaceRepository.GetAllAsync(notification.DomainEvent.Tenant.Id);

            if (workspaces.Count == 0)
            {
                return;
            }

            foreach (var workspace in workspaces)
            {
                workspace.Activate();
                await _workspaceRepository.UnitOfWork.SaveAggregateRootAsync(workspace);
            }

            _logger.LogInformation("Successfully activated {0} workspaces", workspaces.Count);
        }
    }
}
