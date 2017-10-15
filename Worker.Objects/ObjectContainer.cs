﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;
using Worker.Objects.Messages;
using Worker.Objects.Missions;
using Worker.Objects.Research;
using Worker.Objects.Ships;

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

        public Planet CurrentSelectedPlanet { get; set; }
        public List<MissionBase> Missions { get; set; } = new List<MissionBase>();
        public List<Planet> PlayerPlanets { get; set; } = new List<Planet>();
        public List<BuildingBase> PlayerBuildings { get; set; } = new List<BuildingBase>();
        public List<TechnologyBase> PlayerTechnologies { get; set; } = new List<TechnologyBase>();
        public List<ShipBase> PlayerFleet { get; set; } = new List<ShipBase>();

        public ObservableCollection<GalaxyPlanetInfo> GalaxyPlanets { get; set; } = new ObservableCollection<GalaxyPlanetInfo>();
        public List<MessageBase> Messages { get; set; } = new List<MessageBase>();

        public BuildingBase GetBuilding(Planet planet, BuildingType type)
        {
            return PlayerBuildings.First(b => b.BelongsTo.Id == planet.Id && b.BuildingType == type);
        }

        public TechnologyBase GetTechnology(Planet planet, TechnologyType type)
        {
            return PlayerTechnologies.First(t => t.BelongsTo.Id == planet.Id && t.TechnologyType == type);
        }

        public ShipBase GetShip(Planet planet, ShipType type)
        {
            return PlayerFleet.First(s => s.BelongsTo.Id == planet.Id && s.ShipType == type);
        }

        public Planet GetPlanet(int planetId)
        {
            return PlayerPlanets.First(p => p.Id == planetId);
        }
    }
}