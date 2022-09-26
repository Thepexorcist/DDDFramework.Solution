using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Provides a common object for implementing an identity for entities.
    /// </summary>
    /// <typeparam name="TType">The type of the identity</typeparam>
    public class IdentityBase<TType> : ValueObjectBase, IEquatable<IdentityBase<TType>> 
        where TType : IEquatable<TType>
    {
        #region Fields



        #endregion

        #region Properties

        public TType Value { get; set; }

        #endregion

        #region Constructors

        public IdentityBase(TType id)
        {
            Value = id;
        }

        #endregion

        #region Public methods

        public bool Equals(IdentityBase<TType> other)
        {
            if (object.ReferenceEquals(null, other)) return false;
            if (object.ReferenceEquals(this, other.Value)) return true;
            return this.Value.Equals(other.Value);
        }

        public override bool Equals(object anotherObject)
        {
            return ((IEquatable<IdentityBase<TType>>)this).Equals(anotherObject as IdentityBase<TType>);
        }

        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() * 907) + this.Value.GetHashCode();
        }

        public static bool operator ==(IdentityBase<TType> type1, IdentityBase<TType> type2)
        {
            return ((IEquatable<IdentityBase<TType>>)type1).Equals(type2);
        }

        public static bool operator !=(IdentityBase<TType> type1, IdentityBase<TType> type2)
        {
            return !((IEquatable<IdentityBase<TType>>)type1).Equals(type2);
        }

        #endregion

        #region Protected methods

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        #endregion
    }
}
