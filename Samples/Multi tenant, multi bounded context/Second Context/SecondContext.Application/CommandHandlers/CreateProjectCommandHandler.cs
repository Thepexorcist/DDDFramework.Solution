using MediatR;
using SecondContext.Application.Commands;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Tenant;

namespace SecondContext.Application.CommandHandlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, bool>
    {
        #region Fields

        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Constructors

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        #endregion

        public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);

            var projectId = await _projectRepository.GetNextIdentityAsync();
            var project = new Project(tenantId, projectId, request.ProjectNumber, request.DisplayName);
            await _projectRepository.AddAsync(project);
            var result = await _projectRepository.UnitOfWork.SaveAggregateRootAsync(project, cancellationToken); ;
            return result;
        }
    }
}
