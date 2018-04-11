using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public static partial class UniverseObjectsGameObjects
    {
        public static List<GameObject> gameObjects = new List<GameObject>()
        {
            new PlayerObject
            {
                Id = 210,
                Name = "Food",
                SpaceTimeLocationId = 4,
                Description = "Energy to help you survive.",
                Type = PlayerObjectType.Food,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new PlayerObject
            {
                Id = 212,
                Name = "Food",
                SpaceTimeLocationId = 8,
                Description = "Energy to help you survive.",
                Type = PlayerObjectType.Food,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new PlayerObject
            {
                Id = 214,
                Name = "Food",
                SpaceTimeLocationId = 12,
                Description = "Energy to help you survive.",
                Type = PlayerObjectType.Food,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new PlayerObject
            {
                Id = 220,
                Name = "Water",
                SpaceTimeLocationId = 4,
                Description = "Something to keep you hydrated.",
                Type = PlayerObjectType.Water,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new PlayerObject
            {
                Id = 222,
                Name = "Water",
                SpaceTimeLocationId = 8,
                Description = "Something to keep you hydrated.",
                Type = PlayerObjectType.Water,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new PlayerObject
            {
                Id = 224,
                Name = "Water",
                SpaceTimeLocationId = 12,
                Description = "Something to keep you hydrated.",
                Type = PlayerObjectType.Water,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new PlayerObject
            {
                Id = 230,
                Name = "The Key",
                SpaceTimeLocationId = 8,
                Description = "The Key to unlock Room 63.",
                Type = PlayerObjectType.Treasure,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            }
        };

    }
}
