using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Resource
{
    public class MetalMine : BuildingBase
    {
        public MetalMine(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.MetalMine;
        public override int BaseMetalCost => 60;
        public override int BaseCrystalCost => 15;
        public override int BaseDeuteriumCost => 0;
        public override int EnergyConsumption => (int)(10 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
        public override double CostIncreaseFactor => 1.5;

        public int MetalProduction => (int)(UniverseSpeed * 30 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
    }
}
