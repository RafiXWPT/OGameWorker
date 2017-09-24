using System;
using System.Collections.Generic;
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
    }
}
