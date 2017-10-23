using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Fighter
{
    public class LightFighter : ShipBase
    {
        public LightFighter(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType Type => ShipType.LightFighter;
        public override int MetalCost => 3000;
        public override int CrystalCost => 1000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 50;
        public override int FuelConsumption => 20;
        public override int StructuralIntegrity => 4000;
    }
}