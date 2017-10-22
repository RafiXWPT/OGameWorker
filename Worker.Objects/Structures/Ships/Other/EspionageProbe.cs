using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Ships.Other
{
    public class EspionageProbe : ShipBase
    {
        public EspionageProbe(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Other;
        public override ShipType ShipType => ShipType.EspionageProbe;
        public override int MetalCost => 0;
        public override int CrystalCost => 1000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 5;
        public override int FuelConsumption => 1;
        public override int StructuralIntegrity => 1000;
    }
}