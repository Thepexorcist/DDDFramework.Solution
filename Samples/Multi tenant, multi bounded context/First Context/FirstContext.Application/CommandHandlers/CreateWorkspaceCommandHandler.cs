using FirstContext.Application.Commands;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.CommandHandlers
{
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, bool>
    {
        #region Fields

        private readonly ITenantRepository _tenantRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        #endregion

        #region Constructors

        public CreateWorkspaceCommandHandler(ITenantRepository tenantRepository, IWorkspaceRepository workspaceRepository)
        {
            _tenantRepository = tenantRepository;
            _workspaceRepository = workspaceRepository;
        }

        #endregion

        public async Task<bool> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var tenant = await _tenantRepository.GetAsync(tenantId);

            var workspaceId = await _workspaceRepository.GetNextIdentityAsync();
            var workspace = tenant.RegisterWorkspace(workspaceId, request.Name);
            await _workspaceRepository.AddAsync(workspace);
            var result = await _workspaceRepository.UnitOfWork.SaveAggregateRootAsync(workspace, cancellationToken); ;
            return result;
        }
    }
}
