using MediatR;
using SecondContext.Application.Commands;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Tenant;

namespace SecondContext.Application.CommandHandlers
{
    public class PublishProjectCommandHandler : IRequestHandler<PublishProjectCommand, bool>
    {
        #region Fields

        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Constructors

        public PublishProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        #endregion

        public async Task<bool> Handle(PublishProjectCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);

            var project = await _projectRepository.GetByProjectNumberAsync(tenantId, request.ProjectNumber);
            project.Publish();
            await _projectRepository.UpdateAsync(project);
            var result = await _projectRepository.UnitOfWork.SaveAggregateRootAsync(project, cancellationToken); ;
            return result;
        }
    }
}
