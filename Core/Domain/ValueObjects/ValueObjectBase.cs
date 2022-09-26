using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    /// <summary>
    /// In DDD a value object is an object without an identity. It is defined by its properties.
    /// </summary>
    public abstract class ValueObjectBase
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Constructors



        #endregion

        #region Private and protected methods

        protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        #endregion

        #region Internal and public methods

        public static bool operator ==(ValueObjectBase type1, ValueObjectBase type2)
        {
            var obj = type1 as object;
            if (obj == null)
            {
                var obj2 = type2 as object;

                if (obj2 == null)
                {
                    return true;
                }

                return false;
            }

            return type1.Equals(type2);
        }

        public static bool operator !=(ValueObjectBase type1, ValueObjectBase type2)
        {
            return !type1.Equals(type2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObjectBase other = (ValueObjectBase)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public ValueObjectBase? GetCopy()
        {
            return this.MemberwiseClone() as ValueObjectBase;
        }

        #endregion
    }
}
