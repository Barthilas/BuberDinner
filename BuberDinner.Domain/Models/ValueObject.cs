using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Models
{
    // Can be found online.
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityComponents();
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;
            return GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        // two valueobject with the same properties to return same hashcode
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}