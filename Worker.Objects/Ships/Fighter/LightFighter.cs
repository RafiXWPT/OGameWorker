using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Ships.Fighter
{
    public class LightFighter : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.LightFighter;
        public override int MetalCost { get; }
        public override int CrystalCost { get; }
        public override int DeuteriumCost { get; }
        public override int Capacity { get; }
        public override int FuelConsumption { get; }
        public override int Quantity { get; set; } = 1;
    }
}
