using FirstContext.Application.Commands;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.CommandHandlers
{
    public class PublishProjectCommandHandler : IRequestHandler<PublishProjectCommand, bool>
    {
        #region Fields

        private readonly IWorkspaceRepository _workspaceRepository;

        #endregion

        #region Constructors

        public PublishProjectCommandHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        #endregion

        public async Task<bool> Handle(PublishProjectCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var workspaceId = new WorkspaceId(request.WorkspaceId);

            var workspace = await _workspaceRepository.GetAsync(tenantId, workspaceId);
            var projectId = new ProjectId(request.ProjectId);
            workspace.PublishProject(projectId);
            var result = await _workspaceRepository.UnitOfWork.SaveAggregateRootAsync(workspace, cancellationToken); ;
            return result;
        }
    }
}
