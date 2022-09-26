using Domain.Entities;
using SecondContext.Domain.Aggregates.ProjectAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Aggregates.ProjectAggregate.Entities
{
    public class Document : EntityBase<DocumentId>
    {
        #region Fields

        private readonly List<Revision> _revisionHistory;

        #endregion

        #region Properties

        public string Name { get; }
        public string Description { get; private set; }
        public string URL { get; private set; }
        public int Version { get; private set; }
        public IReadOnlyCollection<Revision> RevisionHistory => _revisionHistory;

        #endregion

        protected Document()
        {
            _revisionHistory = new List<Revision>();
        }

        internal Document(DocumentId documentId, string name, string description, string url) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new SecondContextDomainException("name can not be empty");
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new SecondContextDomainException("url cant not be empty");
            }

            Id = documentId;
            Name = name;
            Description = description;
            URL = url;
            Version = 1;

            var revision = new Revision("Created", Version);
            _revisionHistory.Add(revision);
        }

        internal void Update(string comment)
        {
            Version++;
            var revision = new Revision(comment, Version);
            _revisionHistory.Add(revision);
        }
    }
}
