using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Fighter
{
    public class Battlecruiser : ShipBase
    {
        public Battlecruiser(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.Battlecruiser;
        public override int MetalCost => 30000;
        public override int CrystalCost => 40000;
        public override int DeuteriumCost => 15000;
        public override int Capacity => 750;
        public override int FuelConsumption => 250;
        public override int StructuralIntegrity => 70000;
    }
}