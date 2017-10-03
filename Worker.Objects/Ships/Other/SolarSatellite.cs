using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Other
{
    public class SolarSatellite : ShipBase
    {
        public SolarSatellite(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity,
            techReached, canBuild)
        {
        }

        public override ShipAssignment ShipAssignment => ShipAssignment.Other;
        public override ShipType ShipType => ShipType.SolarSatellite;
        public override int MetalCost => 0;
        public override int CrystalCost => 2000;
        public override int DeuteriumCost => 500;
        public override int Capacity => 0;
        public override int FuelConsumption => 0;
        public override int StructuralIntegrity => 2000;
    }
}