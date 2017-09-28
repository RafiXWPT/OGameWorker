﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Ships.Fighter
{
    public class Battleship : ShipBase
    {
        public override ShipAssignment ShipAssignment => ShipAssignment.Fight;
        public override ShipType ShipType => ShipType.Battleship;
        public override int MetalCost => 45000;
        public override int CrystalCost => 15000;
        public override int DeuteriumCost => 0;
        public override int Capacity => 1500;
        public override int FuelConsumption => 500;
        public override int StructuralIntegrity => 60000;


        public Battleship(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, quantity, techReached, canBuild)
        {
        }
    }
}
