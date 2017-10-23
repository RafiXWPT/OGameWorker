using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Transport
{
    public class SmallCargo : ShipBase
    {
        public SmallCargo(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Transport;
        public override ShipType Type => ShipType.SmallCargo;
        public override int MetalCost => 2000;
        public override int CrystalCost => 2000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 5000;
        public override int FuelConsumption => 10;
        public override int StructuralIntegrity => 4000;
    }
}