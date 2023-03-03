using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using BuberDinner.Domain.Models;

namespace BuberDinner.Domain.Menu
{
    public sealed class Menu : AggregateRoot<MenuId>
    {
        private readonly List<MenuSection> _sections = new();
        private readonly List<DinnerId> _dinnerIds = new();
        private readonly List<MenuReviewId> _menuReviewIds = new();

        public string Name { get; }
        public string Description { get; }
        public AverageRating AverageRating { get; }

        public HostId HostId { get; }

        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyList<MenuSection> DinnerIds => _sections.AsReadOnly();
        public IReadOnlyList<MenuSection> MenuReviewIds => _sections.AsReadOnly();

        public DateTime CreatedDateTime { get; }
        public DateTime UpdatedDateTime { get; }

        private Menu(MenuId menuId,
                    string name,
                    string description,
                    AverageRating rating,
                    HostId hostId,
                    DateTime createdDateTime,
                    DateTime updatedDateTime)
                    : base(menuId)
        {
            Name = name;
            Description = description;
            AverageRating = rating;
            HostId = hostId;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Menu Create(
            string name,
            string description,
            HostId hostId)
        {
            return new(
                MenuId.CreateUnique(),
                name,
                description,
                AverageRating.CreateNew(),
                hostId,
                DateTime.UtcNow,
                DateTime.UtcNow
            );
        }
    }
}