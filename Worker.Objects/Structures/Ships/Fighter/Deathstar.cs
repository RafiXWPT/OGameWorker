using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Fighter
{
    public class Deathstar : ShipBase
    {
        public Deathstar(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType Type => ShipType.Deathstar;
        public override int MetalCost => 5000000;
        public override int CrystalCost => 4000000;
        public override int DeuteriumCost => 1000000;
        public override int Capacity => 1000000;
        public override int FuelConsumption => 1;
        public override int StructuralIntegrity => 9000000;
    }
}