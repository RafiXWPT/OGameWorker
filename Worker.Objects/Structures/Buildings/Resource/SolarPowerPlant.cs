using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Buildings.Resource
{
    public class SolarPowerPlant : BuildingBase
    {
        public SolarPowerPlant(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType Type => BuildingType.SolarPowerPlant;
        public override int BaseMetalCost => 75;
        public override int BaseCrystalCost => 30;
        public override int BaseDeuteriumCost => 0;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 1.5;

        public int EnergyProduction => (int) (20 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
    }
}