using Domain.Entities;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainServices.Interfaces;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities
{
    public class Project : EntityBase<ProjectId>
    {
        #region Fields



        #endregion

        #region Properties

        public string UniqueProjectNumber { get; }
        public string Name { get; private set; }
        public DateTime Created { get; }
        public bool IsPublished { get; private set; }

        #endregion

        #region Constructors

        protected Project(string name)
        {
            Created = DateTime.Now;
            Name = name;
        }

        internal Project(string name, IUniqueProjectNumberGenerator uniqueProjectNumberGenerator)
            : this(name)
        {
            Id = new ProjectId(default);
            UniqueProjectNumber = uniqueProjectNumberGenerator.GenerateProjectNumber();
        }

        #endregion

        #region Internal methods

        internal void ChangeName(string name)
        {
            if (name == Name)
            {
                return;
            }

            Name = name;
        }

        internal void Publish()
        {
            if (IsPublished)
            {
                throw new FirstContextDomainException("Project is already published");
            }

            IsPublished = true;
        }

        #endregion
    }
}
