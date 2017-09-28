using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;
using Worker.Objects.Helpers;

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
        Battlecruiser = 215,
        Destroyer = 213,
        Deathstar = 214,
        Bomber = 211,
        Recycler = 209,
        EspionageProbe = 210,
        SolarSatellite = 212,
        ColonyShip = 208
    }

    public static class MilitaryShips
    {
        public static List<ShipType> List => new List<ShipType>
        {
            ShipType.LightFighter,
            ShipType.HeavyFighter,
            ShipType.Cruiser,
            ShipType.Battleship,
            ShipType.Battlecruiser,
            ShipType.Destroyer,
            ShipType.Deathstar,
            ShipType.Bomber
        };
    }

    public static class CivilShips
    {
        public static List<ShipType> List => new List<ShipType>
        {
            ShipType.SmallCargo,
            ShipType.LargeCargo,
            ShipType.Recycler,
            ShipType.EspionageProbe,
            ShipType.SolarSatellite,
            ShipType.ColonyShip
        };
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
        public abstract int StructuralIntegrity { get; }
        public int Quantity { get; set; }

        public int UniverseSpeed { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["UNIVERSE_SPEED"]);

        public bool TechReached { get; set; }
        private bool _canBuild;

        public bool CanBuild
        {
            get => TechReached && _canBuild;
            set => _canBuild = value;
        }

        public Planet BelongsTo { get; }

        public TimeSpan CreatingTimeDuration => TimeSpan.FromHours(StructuralIntegrity / (UniverseSpeed * 2500 * (1 + PlanetCoreBuildingsHelper.GetPlanetShipyard(BelongsTo).CurrentLevel) * Math.Pow(2, 0)));

        protected ShipBase(Planet planet, int quantity, bool techReached, bool canBuild)
        {
            BelongsTo = planet;
            Quantity = quantity;
            TechReached = techReached;
            CanBuild = canBuild;
        }
    }
}
