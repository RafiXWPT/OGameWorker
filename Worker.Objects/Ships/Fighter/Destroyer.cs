﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Fighter
{
    public class Destroyer : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.Destroyer;
        public override int MetalCost => 60000;
        public override int CrystalCost => 50000;
        public override int DeuteriumCost => 15000;
        public override int Capacity => 2000;
        public override int FuelConsumption => 1000;
        public override int StructuralIntegrity => 110000;


        public Destroyer(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
