using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Resource
{
    public class FusionReactor : BuildingBase
    {
        public FusionReactor(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.FusionReactor;
        public override int BaseMetalCost => 900;
        public override int BaseCrystalCost => 360;
        public override int BaseDeuteriumCost => 180;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 1.8;

        public int EnergyProduction => (int) (30 * CurrentLevel * Math.Pow(1.05 * (0.01 * 1), CurrentLevel));
        public int DeuteriumConsumption => (int) (10 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
    }
}