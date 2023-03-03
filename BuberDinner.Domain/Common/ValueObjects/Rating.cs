using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Domain.Models;

namespace BuberDinner.Domain.Common.ValueObjects
{
    public class Rating : ValueObject
    {
        public double Value { get;}
        public Rating(double value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}