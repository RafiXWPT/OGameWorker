using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Fighter
{
    public class Bomber : ShipBase
    {
        public Bomber(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.Bomber;
        public override int MetalCost => 50000;
        public override int CrystalCost => 25000;
        public override int DeuteriumCost => 15000;
        public override int Capacity => 500;
        public override int FuelConsumption => 1000;
        public override int StructuralIntegrity => 75000;
    }
}