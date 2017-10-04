using System.Collections.Generic;
using System.Linq;
using Worker.Objects.Buildings;
using Worker.Objects.Resources;
using Worker.Objects.Ships;

namespace Worker.Objects.Galaxy
{
    public class Planet
    {
        public Planet(string name, PlanetPosition position) : this(0, name, position, new PlanetTemperature())
        {
        }

        public Planet(int id, string name, PlanetPosition position, PlanetTemperature temperature) : this(id, name,
            position, temperature, new Metal(0), new Crystal(0), new Deuterium(0))
        {
        }

        public Planet(int id, string name, PlanetPosition position, PlanetTemperature temperature, Metal metal,
            Crystal crystal, Deuterium deuterium)
        {
            Id = id;
            Name = name;
            Temperature = temperature;
            Position = position;
            Metal = metal;
            Crystal = crystal;
            Deuterium = deuterium;
        }

        public int Id { get; set; }
        public PlanetTemperature Temperature { get; set; }
        public PlanetPosition Position { get; set; }
        public string Name { get; set; }
        public Metal Metal { get; set; }
        public Crystal Crystal { get; set; }
        public Deuterium Deuterium { get; set; }
        public int TotalResources => (int)(Metal.Amount + Crystal.Amount + Deuterium.Amount);

        public List<BuildingBase> PlanetBuildings => ObjectContainer.Instance.PlayerBuildings
            .Where(b => b.BelongsTo.Id == Id && b.CurrentLevel > 0)
            .ToList();

        public List<ShipBase> PlanetShips => ObjectContainer.Instance.PlayerShips
            .Where(s => s.BelongsTo.Id == Id && s.Quantity > 0)
            .ToList();

        public struct PlanetTemperature
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public double Average => (Min + Max) / 2.0;
        }

        public struct PlanetPosition
        {
            public int Galaxy { get; set; }
            public int System { get; set; }
            public int Planet { get; set; }
        }
    }
}