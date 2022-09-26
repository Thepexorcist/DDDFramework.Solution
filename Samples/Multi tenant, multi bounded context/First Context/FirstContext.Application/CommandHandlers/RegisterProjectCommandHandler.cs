using FirstContext.Application.Commands;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainServices.Interfaces;
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
    public class RegisterProjectCommandHandler : IRequestHandler<RegisterProjectCommand, bool>
    {
        #region Fields

        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IUniqueProjectNumberGenerator _uniqueProjectNumberGenerator;

        #endregion

        #region Constructors

        public RegisterProjectCommandHandler(IWorkspaceRepository workspaceRepository, IUniqueProjectNumberGenerator uniqueProjectNumberGenerator)
        {
            _workspaceRepository = workspaceRepository;
            _uniqueProjectNumberGenerator = uniqueProjectNumberGenerator;
        }

        #endregion

        public async Task<bool> Handle(RegisterProjectCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var workspaceId = new WorkspaceId(request.WorkspaceId);

            var workspace = await _workspaceRepository.GetAsync(tenantId, workspaceId);

            workspace.RegisterProject(request.Name, _uniqueProjectNumberGenerator);
            await _workspaceRepository.UpdateAsync(workspace);
            var result = await _workspaceRepository.UnitOfWork.SaveAggregateRootAsync(workspace, cancellationToken);
            return result;
        }
    }
}
