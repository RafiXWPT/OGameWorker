using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Messages;
using Worker.Objects.Missions;
using Worker.Objects.Research;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Ships;

namespace Worker.Objects
{
    public class ObjectContainer
    {
        private static readonly Lazy<ObjectContainer> Lazy = new Lazy<ObjectContainer>(() => new ObjectContainer());
        public bool Initialized { get; set; }

        private ObjectContainer()
        {
        }

        public static ObjectContainer Instance => Lazy.Value;

        public PlayerPlanet CurrentSelectedPlanet { get; set; }
        public List<MissionBase> Missions { get; set; } = new List<MissionBase>();
        public List<PlayerPlanet> PlayerPlanets { get; set; } = new List<PlayerPlanet>();
        public List<BuildingBase> PlayerBuildings { get; set; } = new List<BuildingBase>();
        public List<TechnologyBase> PlayerTechnologies { get; set; } = new List<TechnologyBase>();
        public List<ShipBase> PlayerFleet { get; set; } = new List<ShipBase>();
        public List<DefenseBase> PlayerDefense { get; set; } = new List<DefenseBase>();

        public ObservableCollection<Planet> GalaxyPlanets { get; set; } = new ObservableCollection<Planet>();
        public List<EnemyPlanet> EnemyGalaxyPlanets => GalaxyPlanets.Cast<EnemyPlanet>().ToList();

        public List<MessageBase> Messages { get; set; } = new List<MessageBase>();

        public BuildingBase GetBuilding(Planet planet, BuildingType type)
        {
            return PlayerBuildings.First(b => b.BelongsTo.Id == planet.Id && b.Type == type);
        }

        public TechnologyBase GetTechnology(Planet planet, TechnologyType type)
        {
            return PlayerTechnologies.First(t => t.BelongsTo.Id == planet.Id && t.Type == type);
        }

        public ShipBase GetShip(Planet planet, ShipType type)
        {
            return PlayerFleet.First(s => s.BelongsTo.Id == planet.Id && s.Type == type);
        }

        public DefenseBase GetDefense(Planet planet, DefenseType type)
        {
            return PlayerDefense.First(d => d.BelongsTo.Id == planet.Id && d.Type == type);
        }

        public PlayerPlanet GetPlayerPlanet(int planetId)
        {
            return PlayerPlanets.First(p => p.Id == planetId);
        }

        public TType GetGalaxyPlanet<TType>(int planetId) where TType: class
        {
            return GalaxyPlanets.First(p => p.Id == planetId) as TType;
        }
    }
}