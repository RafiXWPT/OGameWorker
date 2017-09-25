using System;
using System.Collections.Generic;
using System.Linq;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;

namespace Worker.Objects
{
    public class ObjectContainer
    {
        private static readonly Lazy<ObjectContainer> Lazy = new Lazy<ObjectContainer>(() => new ObjectContainer());
        public static ObjectContainer Instance => Lazy.Value;

        private ObjectContainer() { }

        public List<Planet> PlayerPlanets { get; set; } = new List<Planet>();
        public List<BuildingBase> PlayerBuildings { get; set; } = new List<BuildingBase>();

        public BuildingBase GetBuilding(Planet planet, BuildingType type) => PlayerBuildings.First(b => b.BelongsTo.Id == planet.Id && b.BuildingType == type);
        public Planet GetPlanet(int planetId) => PlayerPlanets.First(p => p.Id == planetId);
    }
}
