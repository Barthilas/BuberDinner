using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.Models;

namespace BuberDinner.Domain.Menu.Entities
{
    public sealed class MenuSection : Entity<MenuSectionId>
    {

        private readonly List<MenuItem> _items = new();

        public string Name { get; }
        public string Description { get; }

        // can be cast back into list and changed.. in newer C# ver. there is a way for true readonly.
        public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

        private MenuSection(
            MenuSectionId menuSectionId,
            string name,
            string description)
            : base(menuSectionId)
        {
            Name = name;
            Description = description;
        }

        private static MenuSection Create(
            string name,
            string description)
        {
            return new(
                MenuSectionId.CreateUnique(),
                name,
                description);
        }
        
    }
}