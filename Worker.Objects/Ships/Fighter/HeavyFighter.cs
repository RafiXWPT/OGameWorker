using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Fighter
{
    public class HeavyFighter : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.HeavyFighter;
        public override int MetalCost => 6000;
        public override int CrystalCost => 4000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 100;
        public override int FuelConsumption => 75;
        public override int StructuralIntegrity => 10000;


        public HeavyFighter(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
