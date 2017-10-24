using System.Collections.Generic;
using System.Linq;
using Worker.Objects.Resources;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Ships;

namespace Worker.Objects.Galaxy.Planet
{
    public struct PlanetTemperature
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public double Average => (Min + Max) / 2.0;
    }

    public class PlayerPlanet : Planet
    {
        public PlayerPlanet(int id, string name, Position position) : this(id, name, position, new PlanetTemperature()) { }

        public PlayerPlanet(int id, string name, Position position, PlanetTemperature temperature) : this(id, name, position, temperature, new Metal(0), new Crystal(0), new Deuterium(0)) { }

        public PlayerPlanet(int id, string name, Position position, PlanetTemperature temperature, Metal metal, Crystal crystal, Deuterium deuterium) : base(id, name, position, metal, crystal, deuterium)
        {
            Temperature = temperature;
            Type = PlanetType.Self;
        }

        public PlanetTemperature Temperature { get; set; }

        public override List<BuildingBase> Buildings => ObjectContainer.Instance.PlayerBuildings
            .Where(b => b.BelongsTo.Id == Id && b.CurrentLevel > 0)
            .ToList();

        public override List<ShipBase> Ships => ObjectContainer.Instance.PlayerFleet
            .Where(s => s.BelongsTo.Id == Id && s.Quantity > 0)
            .ToList();

        public override List<DefenseBase> Defenses => ObjectContainer.Instance.PlayerDefense
            .Where(d => d.BelongsTo.Id == Id && d.Amount > 0)
            .ToList();
    }
}