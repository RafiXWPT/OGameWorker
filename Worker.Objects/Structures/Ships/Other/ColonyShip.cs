using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Other
{
    public class ColonyShip : ShipBase
    {
        public ColonyShip(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Other;
        public override ShipType ShipType => ShipType.ColonyShip;
        public override int MetalCost => 10000;
        public override int CrystalCost => 20000;
        public override int DeuteriumCost => 10000;
        public override int Capacity => 7500;
        public override int FuelConsumption => 1000;
        public override int StructuralIntegrity => 30000;
    }
}