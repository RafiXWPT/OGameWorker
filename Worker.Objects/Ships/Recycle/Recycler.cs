using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Recycle
{
    public class Recycler : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Recycle;
        public override ShipType ShipType => ShipType.Recycler;
        public override int MetalCost => 10000;
        public override int CrystalCost => 6000;
        public override int DeuteriumCost => 2000;
        public override int Capacity => 20000;
        public override int FuelConsumption => 300;
        public override int StructuralIntegrity => 16000;

        public Recycler(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
