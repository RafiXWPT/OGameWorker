using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Other
{
    public class EspionageProbe : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Other;
        public override ShipType ShipType => ShipType.EspionageProbe;
        public override int MetalCost => 0;
        public override int CrystalCost => 1000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 5;
        public override int FuelConsumption => 1;
        public override int StructuralIntegrity => 1000;

        public EspionageProbe(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
