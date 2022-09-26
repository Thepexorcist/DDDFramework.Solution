using FirstContext.Application.Commands;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.CommandHandlers
{
    public class DeactivateWorkspaceCommandHandler : IRequestHandler<DeactivateWorkspaceCommand, bool>
    {
        #region Fields

        private readonly IWorkspaceRepository _workspaceRepository;

        #endregion

        #region Constructors

        public DeactivateWorkspaceCommandHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        #endregion

        public async Task<bool> Handle(DeactivateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var workspaceId = new WorkspaceId(request.WorkspaceId);

            var workspace = await _workspaceRepository.GetAsync(tenantId, workspaceId);
            workspace.Deactivate();
            var result = await _workspaceRepository.UnitOfWork.SaveAggregateRootAsync(workspace, cancellationToken); ;
            return result;
        }
    }
}
