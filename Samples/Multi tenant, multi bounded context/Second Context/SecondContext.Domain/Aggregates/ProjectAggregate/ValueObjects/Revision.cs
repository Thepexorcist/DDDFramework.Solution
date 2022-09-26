using Domain.ValueObjects;
using SecondContext.Domain.Aggregates.ProjectAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Aggregates.ProjectAggregate.ValueObjects
{
    public sealed class Revision : ValueObjectBase
    {
        public string Comment { get; }
        public int Version { get; }

        public Revision(string comment, int version)
        {
            Comment = comment;
            Version = version;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Comment;
            yield return Version;
        }
    }
}
