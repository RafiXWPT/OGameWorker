﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Transport
{
    public class SmallCargo : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Transport;
        public override ShipType ShipType => ShipType.SmallCargo;
        public override int MetalCost => 2000;
        public override int CrystalCost => 2000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 5000;
        public override int FuelConsumption => 10;
        public override int StructuralIntegrity => 4000;

        public SmallCargo(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
