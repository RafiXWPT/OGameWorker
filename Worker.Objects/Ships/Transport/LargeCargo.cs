using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Transport
{
    public class LargeCargo : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Transport;
        public override ShipType ShipType => ShipType.LargeCargo;
        public override int MetalCost => 6000;
        public override int CrystalCost => 6000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 25000;
        public override int FuelConsumption => 50;
        public override int StructuralIntegrity => 12000;

        public LargeCargo(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
