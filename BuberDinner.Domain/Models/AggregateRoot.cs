using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Models
{
    // sort of a wrapper around entity
    public class AggregateRoot<TId> : Entity<TId>
        where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }
    }
}