using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Ships.Fighter
{
    public class Cruiser : ShipBase
    {
        public Cruiser(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType Type => ShipType.Cruiser;
        public override int MetalCost => 20000;
        public override int CrystalCost => 7000;
        public override int DeuteriumCost => 2000;
        public override int Capacity => 800;
        public override int FuelConsumption => 300;
        public override int StructuralIntegrity => 27000;
    }
}