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

    public abstract class BuildingBase
    {
        public abstract BuildingType BuildingType { get; }
        public Planet BelongsTo { get; set; }
        public int CurrentLevel { get; set; }
        public bool CanBuild { get; set; }
        public double MetalCost { get; set; }
        public double CrystalCost { get; set; }
        public double DeuteriumCost { get; set; }
        public TimeSpan UpgradeTimeDuration { get; set; }
        private bool _techReached;

        protected BuildingBase(Planet belongsTo, int currentLevel, bool techReached)
        {
            BelongsTo = belongsTo;
            CurrentLevel = currentLevel;
            _techReached = techReached;
        }
    }
}
