using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Resource
{
    public class SolarSatellite : BuildingBase
    {
        public SolarSatellite(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.SolarSatellite;
        public override int BaseMetalCost => 0;
        public override int BaseCrystalCost => 0;
        public override int BaseDeuteriumCost => 0;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 0;

        public int EnergyProduction => (int)Math.Floor((BelongsTo.Temperature.Average+160)/6);
    }
}
