using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Interfaces
{
    /// <summary>
    /// In domain driven design an entity is defined as having an identity.
    /// </summary>
    /// <typeparam name="TIdentity">The identity of the entity.</typeparam>
    public interface IEntity<TIdentity> 
    {
        public TIdentity Id { get; }
    }
}
