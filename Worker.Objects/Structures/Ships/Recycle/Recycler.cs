using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Recycle
{
    public class Recycler : ShipBase
    {
        public Recycler(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Recycle;
        public override ShipType Type => ShipType.Recycler;
        public override int MetalCost => 10000;
        public override int CrystalCost => 6000;
        public override int DeuteriumCost => 2000;
        public override int Capacity => 20000;
        public override int FuelConsumption => 300;
        public override int StructuralIntegrity => 16000;
    }
}