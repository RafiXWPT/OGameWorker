using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Fighter
{
    public class Deathstar : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.Deathstar;
        public override int MetalCost => 5000000;
        public override int CrystalCost => 4000000;
        public override int DeuteriumCost => 1000000;
        public override int Capacity => 1000000;
        public override int FuelConsumption => 1;
        public override int StructuralIntegrity => 9000000;

        public Deathstar(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
