using MediatR;
using SecondContext.Application.Commands;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Entities;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Employees.Interfaces;
using SecondContext.Domain.Tenant;

namespace SecondContext.Application.CommandHandlers
{
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, bool>
    {
        #region Fields

        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Constructors

        public AddDocumentCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        #endregion

        public async Task<bool> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var projectId = new ProjectId(request.ProjectId);
            var documentId = new DocumentId(request.DocumentId);

            var project = await _projectRepository.GetAsync(tenantId, projectId);
            project.AddDocument(documentId, request.Name, request.Description, request.URL);
            await _projectRepository.UpdateAsync(project);
            var result = await _projectRepository.UnitOfWork.SaveAggregateRootAsync(project, cancellationToken); ;
            return result;
        }
    }
}
