using MediatR;
using SecondContext.Application.Commands;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Employees.Interfaces;
using SecondContext.Domain.Tenant;

namespace SecondContext.Application.CommandHandlers
{
    public class AssignElectricianCommandHandler : IRequestHandler<AssignElectricianCommand, bool>
    {
        #region Fields

        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeService _employeeService;

        #endregion

        #region Constructors

        public AssignElectricianCommandHandler(IProjectRepository projectRepository, IEmployeeService employeeService)
        {
            _projectRepository = projectRepository;
            _employeeService = employeeService;
        }

        #endregion

        public async Task<bool> Handle(AssignElectricianCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);
            var projectId = new ProjectId(request.ProjectId);

            var electrician = await _employeeService.FindElectricianByEmployeeNumberAsync(tenantId, request.EmployeeNumber);

            if (electrician.EmployeeNumber < 0)
            {
                throw new ArgumentException("Could not find electrician.");
            }

            var project = await _projectRepository.GetAsync(tenantId, projectId);
            project.AssignElectrician(electrician);
            await _projectRepository.UpdateAsync(project);
            var result = await _projectRepository.UnitOfWork.SaveAggregateRootAsync(project, cancellationToken); ;
            return result;
        }
    }
}
