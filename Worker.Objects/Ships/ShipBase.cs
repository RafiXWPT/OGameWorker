using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Ships
{
    public enum ShipAssignment
    {
        Transport,
        Fight,
        Recycle,
        Other
    }

    public enum ShipType
    {
        SmallCargo = 202,
        LargeCargo = 203,
        LightFighter = 204,
        HeavyFighter = 205,
        Cruiser = 206,
        Battleship = 207,
        Battlecruiser = 208,
        Destroyer = 213,
        Deathstar = 214,
        Bomber = 211,
        Recycler = 209,
        EspionageProbe = 210,
        SolarSatellite = 212,
        ColonyShip = 208
    }

    public abstract class ShipBase
    {
        public abstract ShipAssignment ShipAssignment { get; }
        public abstract ShipType ShipType { get; }
        public abstract int MetalCost { get; }
        public abstract int CrystalCost { get; }
        public abstract int DeuteriumCost { get; }
        public abstract int Capacity { get; }
        public abstract int FuelConsumption { get; }
        public abstract int Quantity { get; set; }
    }
}
