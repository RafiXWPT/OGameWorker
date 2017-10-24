using System;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Buildings.Resource
{
    public class CrystalMine : BuildingBase
    {
        public CrystalMine(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType Type => BuildingType.CrystalMine;
        public override int BaseMetalCost => 48;
        public override int BaseCrystalCost => 24;
        public override int BaseDeuteriumCost => 0;
        public override int EnergyConsumption => (int) (10 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
        public override double CostIncreaseFactor => 1.6;

        public int CrystalProduction => (int) (UniverseSpeed * 20 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
    }
}