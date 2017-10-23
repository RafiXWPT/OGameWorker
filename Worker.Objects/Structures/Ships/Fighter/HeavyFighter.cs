using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Fighter
{
    public class HeavyFighter : ShipBase
    {
        public HeavyFighter(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType Type => ShipType.HeavyFighter;
        public override int MetalCost => 6000;
        public override int CrystalCost => 4000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 100;
        public override int FuelConsumption => 75;
        public override int StructuralIntegrity => 10000;
    }
}