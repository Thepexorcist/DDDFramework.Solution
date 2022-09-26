using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Base entity class that all entity types should inherit from.
    /// </summary>
    /// <typeparam name="TIdentity">The identity type.</typeparam>
    public abstract class EntityBase<TIdentity> : IEntity<TIdentity>, IEquatable<EntityBase<TIdentity>>
    {
        #region Fields



        #endregion

        #region Properties

        public TIdentity Id { get; set; } = default(TIdentity);

        #endregion

        #region Constructors

        protected EntityBase() { }

        public EntityBase(TIdentity identity)
        {
            Id = identity;
        }

        #endregion

        #region Public methods

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} {{ Id = {Id} }}";
        }

        public bool Equals(EntityBase<TIdentity> other)
        {
            return other.Id.Equals(other.Id);
        }

        #endregion
    }
}