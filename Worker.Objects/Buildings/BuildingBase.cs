using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings
{
    public enum BuildingType
    {
        MetalMine = 1,
        CrystalMine = 2,
        DeuteriumExtractor = 3,
        SolarPowerPlant = 4,
        FusionPowerPlant = 12,
        SolarSatellite = 212,
        MetalWarehouse = 22,
        CrystalWarehouse = 23,
        DeuteriumWarehouse = 24,
        RobotFactory = 14,
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
            BuildingType.FusionPowerPlant,
            BuildingType.SolarSatellite
        };
    }

    public static class StationBuildings
    {
        public static List<BuildingType> List { get; } = new List<BuildingType>
        {
            BuildingType.RobotFactory,
            BuildingType.Shipyard,
            BuildingType.ResearchLabolatory
        };
    }

    public static class WarehouseBuildings
    {
        public static List<BuildingType> List { get; } = new List<BuildingType>
        {
            BuildingType.MetalWarehouse,
            BuildingType.CrystalWarehouse,
            BuildingType.DeuteriumWarehouse
        };
    }

    public abstract class BuildingBase
    {
        public abstract BuildingType BuildingType { get; }
        public Planet BelongsTo { get; set; }
        public int CurrentLevel { get; set; }
        public bool TechReached { get; set; }
        private bool _canBuild;

        public bool CanBuild
        {
            get => TechReached && _canBuild;
            set => _canBuild = value;
        }

        public double MetalCost { get; set; }
        public double CrystalCost { get; set; }
        public double DeuteriumCost { get; set; }
        public TimeSpan UpgradeTimeDuration { get; set; }

        protected BuildingBase(Planet belongsTo, int currentLevel, bool techReached, bool canBuild)
        {
            BelongsTo = belongsTo;
            CurrentLevel = currentLevel;
            TechReached = techReached;
            CanBuild = canBuild;
        }
    }
}
