using System.Collections.Generic;
using Worker.Objects.Resources;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Ships;

namespace Worker.Objects.Galaxy.Planet
{
    public enum PlanetType
    {
        Empty,
        Self,
        EnemyUnknown,
        EnemyVacation,
        EnemyNewbie,
        EnemyWeak,
        EnemyInactive,
        EnemyStrong
    }

    public class Planet : GalaxyObject
    {
        public PlanetType Type { get; set; }
        public virtual List<BuildingBase> Buildings { get; }
        public virtual List<ShipBase> Ships { get; }
        public virtual List<DefenseBase> Defenses { get; }

        public Planet(int id, string name, Position position) : base(id, name, position) { }

        public Planet(int id, string name, Position position, Metal metal, Crystal crystal, Deuterium deuterium) : base(id, name, position, metal, crystal, deuterium) { }
    }
}
