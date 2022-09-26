using Domain.Tenancy;
using SecondContext.Domain.Aggregates.ProjectAggregate.Entities;
using SecondContext.Domain.Employees;
using SecondContext.Domain.Tenant;

namespace SecondContext.Domain.Aggregates.ProjectAggregate
{
    public class Project : MultiTenantAggregateRootBase<TenantId, ProjectId>
    {
        #region Fields

        private readonly List<Electrician> _assignedElectricians;
        private readonly List<Document> _documents;

        #endregion

        #region Properties

        public string ProjectNumber { get; }

        /// <summary>
        /// This is the display name that will be shown when listing or displaying details about this project.
        /// This will first be set when a project is created in the first context bounded context.
        /// If project name changes in the first context bounded context this will not be shown here since 
        /// no subscription is set up to listen to those events from that bounded context.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Defines if the project is published from the workspace.
        /// If not no work can be started.
        /// </summary>
        public bool IsPublished { get; private set; }

        public ProjectManager AssignedProjectManager { get; private set; }
        public IReadOnlyCollection<Electrician> AssignedElectricians => _assignedElectricians;
        public IReadOnlyCollection<Document> Documents => _documents;

        #endregion

        #region Constructors

        protected Project(TenantId tenantId, ProjectId id) : base(tenantId, id) 
        {
            _assignedElectricians = new List<Electrician>();
            _documents = new List<Document>();
        }

        public Project(TenantId tenantId, ProjectId id, string projectNumber, string displayName) : this(tenantId, id)
        {
            ProjectNumber = projectNumber;
            DisplayName = displayName;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Changes the display name of the project.
        /// This property is not at all in sync with the project that is created in the first context bounded context.
        /// Changing this here will not affect the project in the first context.
        /// </summary>
        /// <param name="displayName">The display name for the project.</param>
        public void ChangeDisplayName(string displayName)
        {
            if (DisplayName != displayName)
            {
                DisplayName = displayName;
            }
        }

        public void AssignProjectManager(ProjectManager projectManager)
        {
            if (!IsPublished)
            {
                throw new SecondContextDomainException("Can not assign a project manager when the project is not yet published from workspace.");
            }

            AssignedProjectManager = projectManager;
        }

        public void AssignElectrician(Electrician electrician)
        {
            if (!IsPublished)
            {
                throw new SecondContextDomainException("Can not assign an electrician when the project is not yet published from workspace.");
            }

            if (_assignedElectricians.Contains(electrician))
            {
                return;
            }

            _assignedElectricians.Add(electrician);
        }

        public void AddDocument(DocumentId documentId, string name, string description, string url)
        {
            if (!IsPublished)
            {
                throw new SecondContextDomainException("Can add document when the project is not yet published from workspace.");
            }

            if (_documents.Any(x => x.Id == documentId))
            {
                return;
            }

            var document = new Document(documentId, name, description, url);
            _documents.Add(document);
        }

        /// <summary>
        /// Makes a dummy update of an existing document just to show how business logics are handled without the domain aggregates/entities.
        /// </summary>
        /// <param name="documentId">The document to update.</param>
        /// <param name="comment">Any comment regarding the update of the document.</param>
        /// <exception cref="SecondContextDomainException"></exception>
        public void UpdateDocument(DocumentId documentId, string comment)
        {
            if (!IsPublished)
            {
                throw new SecondContextDomainException("Can not update a document when the project is not yet published from workspace.");
            }

            var document = _documents.FirstOrDefault(x => x.Id == documentId);

            if (document == null)
            {
                throw new SecondContextDomainException("Can not find specified document");
            }

            // If comment is empty. Just iterate up the comment.
            if (string.IsNullOrEmpty(comment))
            {
                var revisions = document.RevisionHistory.Count;
                comment = "Comment " + (revisions + 1);
            }

            document.Update(comment);
        }

        /// <summary>
        /// Sets the project to published state.
        /// Assignment of employees to work on the project can now start.
        /// </summary>
        /// <exception cref="SecondContextDomainException"></exception>
        public void Publish()
        {
            if (IsPublished)
            {
                throw new SecondContextDomainException("Project is already published.");
            }

            IsPublished = true;
        }

        #endregion
    }
}
