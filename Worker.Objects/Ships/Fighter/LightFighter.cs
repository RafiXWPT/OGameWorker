using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Fighter
{
    public class LightFighter : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.LightFighter;
        public override int MetalCost => 3000;
        public override int CrystalCost => 1000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 50;
        public override int FuelConsumption => 20;
        public override int StructuralIntegrity => 4000;

        public LightFighter(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
