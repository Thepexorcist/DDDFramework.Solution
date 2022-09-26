using Domain.Tenancy;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainEvents;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainServices.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.ValueObjects;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate
{
    public class Workspace : MultiTenantAggregateRootBase<TenantId, WorkspaceId>
    {
        #region Fields

        private readonly List<Project> _projects;
        private readonly List<LogEntry> _logEntries;
        private const int MAXIMUM_NUMBER_OF_PROJECTS = 3;

        #endregion

        #region Properties

        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<Project> Projects => _projects;
        public IReadOnlyCollection<LogEntry> LogEntries => _logEntries;

        #endregion

        #region Constructors

        protected Workspace(TenantId tenantId, WorkspaceId id) : base(tenantId, id)
        {
            _projects = new List<Project>();
            _logEntries = new List<LogEntry>();
            IsActive = true;
        }

        internal Workspace(TenantId tenantId, WorkspaceId id, string name)
            : this(tenantId, id)
        {
            Name = name;
        }

        #endregion

        #region Public methods

        public void Activate()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            _logEntries.Add(new LogEntry("Workspace activated"));
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            _logEntries.Add(new LogEntry("Workspace deactivated"));
        }

        public void RegisterProject(string projectName, IUniqueProjectNumberGenerator uniqueProjectNumberGenerator)
        {
            if (!IsActive)
            {
                throw new FirstContextDomainException("Can not register a new project on a deactivated workspace.");
            }
            if (_projects.Count == MAXIMUM_NUMBER_OF_PROJECTS)
            {
                throw new FirstContextDomainException(String.Format("A workspace can only contain {0} projects.", MAXIMUM_NUMBER_OF_PROJECTS));
            }

            var exists = _projects.Any(p => p.Name == projectName);

            if (exists)
            {
                throw new FirstContextDomainException("Project with same name already exists");
            }

            var project = new Project(projectName, uniqueProjectNumberGenerator);
            _projects.Add(project);

            AddDomainEvent(new ProjectRegistered(TenantId, Id, project.UniqueProjectNumber, project.Name));
            _logEntries.Add(new LogEntry(String.Format("Project {0} - {1} registered", project.UniqueProjectNumber, project.Name)));
        }

        public void ChangeProjectName(ProjectId projectId, string newName)
        {
            var project = _projects.FirstOrDefault(x => x.Id == projectId);

            if (project == null)
            {
                throw new FirstContextDomainException("Can not find specified project");
            }

            var oldName = project.Name;
            project.ChangeName(newName);

            _logEntries.Add(new LogEntry(String.Format("Project {0} changed name to {1}", oldName, project.Name)));
        }

        public void PublishProject(ProjectId projectId)
        {
            if (!IsActive)
            {
                throw new FirstContextDomainException("Can not publish a project from a deactivated workspace.");
            }

            var project = _projects.FirstOrDefault(x => x.Id == projectId);

            if (project == null)
            {
                throw new FirstContextDomainException("Can not find specified project");
            }

            project.Publish();

            AddDomainEvent(new ProjectPublished(TenantId, Id, project.UniqueProjectNumber));
            _logEntries.Add(new LogEntry(String.Format("Project {0} - {1} published ", project.UniqueProjectNumber, project.Name)));
        }

        #endregion
    }
}
