using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Fighter
{
    public class Cruiser : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.Cruiser;
        public override int MetalCost => 20000;
        public override int CrystalCost => 7000;
        public override int DeuteriumCost => 2000;
        public override int Capacity => 800;
        public override int FuelConsumption => 300;
        public override int StructuralIntegrity => 27000;


        public Cruiser(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
