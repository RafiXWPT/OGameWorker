using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;
using Worker.Objects.Helpers;

namespace Worker.Objects.Buildings
{
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
        RocketSilo = 44
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

    public abstract class BuildingBase
    {
        public abstract BuildingType BuildingType { get; }
        public abstract int BaseMetalCost { get; }
        public abstract int BaseCrystalCost { get; }
        public abstract int BaseDeuteriumCost { get; }
        public abstract int EnergyConsumption { get; }
        public abstract double CostIncreaseFactor { get; }

        public int UniverseSpeed { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["UNIVERSE_SPEED"]);

        public bool TechReached { get; set; }
        private bool _canBuild;

        public bool CanBuild
        {
            get => TechReached && _canBuild;
            set => _canBuild = value;
        }

        public Planet BelongsTo { get; set; }
        public int CurrentLevel { get; set; }


        public int MetalCost => (int)(BaseMetalCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public int CrystalCost => (int)(BaseCrystalCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public int DeuteriumCost => (int)(BaseDeuteriumCost * Math.Pow(CostIncreaseFactor, CurrentLevel));

        public TimeSpan UpgradeTimeDuration => ObjectContainer.Instance.PlayerBuildings.Any()
            ? TimeSpan.FromHours((MetalCost + CrystalCost) /
                                 (UniverseSpeed * 2500 *
                                  (1 + PlanetCoreBuildingsHelper.GetPlanetRoboticsFactory(BelongsTo).CurrentLevel) *
                                  Math.Pow(2, 0)))
            : TimeSpan.MaxValue;

        protected BuildingBase(Planet belongsTo, int currentLevel, bool techReached, bool canBuild)
        {
            BelongsTo = belongsTo;
            CurrentLevel = currentLevel;
            TechReached = techReached;
            CanBuild = canBuild;
        }
    }
}
