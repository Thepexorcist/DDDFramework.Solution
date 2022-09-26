using MediatR;
using SecondContext.Application.Commands;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Employees.Interfaces;
using SecondContext.Domain.Tenant;

namespace SecondContext.Application.CommandHandlers
{
    public class AssignProjectManagerCommandHandler : IRequestHandler<AssignProjectManagerCommand, bool>
    {
        #region Fields

        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeService _employeeService;

        #endregion

        #region Constructors

        public AssignProjectManagerCommandHandler(IProjectRepository projectRepository, IEmployeeService employeeService)
        {
            _projectRepository = projectRepository;
            _employeeService = employeeService;
        }

        #endregion

        public async Task<bool> Handle(AssignProjectManagerCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var projectId = new ProjectId(request.ProjectId);

            var projectManager = await _employeeService.FindProjectManagerByEmployeeNumberAsync(tenantId, request.EmployeeNumber);

            if (projectManager.EmployeeNumber < 0)
            {
                throw new ArgumentException("Could not find project manager.");
            }

            var project = await _projectRepository.GetAsync(tenantId, projectId);
            project.AssignProjectManager(projectManager);
            await _projectRepository.UpdateAsync(project);
            var result = await _projectRepository.UnitOfWork.SaveAggregateRootAsync(project, cancellationToken); ;
            return result;
        }
    }
}
