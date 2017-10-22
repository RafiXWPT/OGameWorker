using System;
using System.Collections.Generic;
using System.Configuration;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures
{
    #region Buildings

    public enum BuildingType
    {
        MetalMine = 1,
        CrystalMine = 2,
        DeuteriumExtractor = 3,
        SolarPowerPlant = 4,
        FusionReactor = 12,
        SolarSatellite = 212,
        MetalStorage = 22,
        CrystalStorage = 23,
        DeuteriumTank = 24,
        RoboticsFactory = 14,
        Shipyard = 21,
        ResearchLabolatory = 31,
        RocketSilo = 44,
        NaniteFactory = 15
    }

    public static class ResourceBuildings
    {
        public static List<BuildingType> List { get; } = new List<BuildingType>
        {
            BuildingType.MetalMine,
            BuildingType.CrystalMine,
            BuildingType.DeuteriumExtractor,
            BuildingType.SolarPowerPlant,
            BuildingType.FusionReactor,
            BuildingType.SolarSatellite
        };
    }

    public static class StationBuildings
    {
        public static List<BuildingType> List { get; } = new List<BuildingType>
        {
            BuildingType.RoboticsFactory,
            BuildingType.Shipyard,
            BuildingType.ResearchLabolatory
        };
    }

    public static class StorageBuildings
    {
        public static List<BuildingType> List { get; } = new List<BuildingType>
        {
            BuildingType.MetalStorage,
            BuildingType.CrystalStorage,
            BuildingType.DeuteriumTank
        };
    }

    #endregion

    #region Defenses

    public enum DefenseType
    {
        RocketLauncher,
        LightLaser,
        HeavyLaser,
        IonCannon,
        GaussCannon,
        PlasmaTurret,
        SmallShield,
        LargeShield,
        AntiBallisticMissile,
        InterplanetaryMissile
    }

    #endregion

    #region Ships

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

    public enum ShipAssignment
    {
        Transport,
        Fight,
        Recycle,
        Other
    }

    #endregion

    public abstract class StructureBase
    {
        protected bool _canBuild;
        public int UniverseSpeed { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["UNIVERSE_SPEED"]);
        public bool TechReached { get; set; }

        public bool CanBuild
        {
            get => TechReached && _canBuild;
            set => _canBuild = value;
        }

        public Planet BelongsTo { get; }

        public abstract TimeSpan CreatingTimeDuration { get; }

        public abstract int MetalCost { get; }
        public abstract int CrystalCost { get; }
        public abstract int DeuteriumCost { get; }

        protected StructureBase(Planet belogsTo, bool canBuild, bool techReached)
        {
            BelongsTo = belogsTo;
            CanBuild = canBuild;
            TechReached = techReached;
        }
    }
}
