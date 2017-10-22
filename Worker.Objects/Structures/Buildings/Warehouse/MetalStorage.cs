using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Buildings.Warehouse
{
    public class MetalStorage : BuildingBase
    {
        public MetalStorage(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.MetalStorage;
        public override int BaseMetalCost => 1000;
        public override int BaseCrystalCost => 0;
        public override int BaseDeuteriumCost => 0;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 2.0;
        public int StorageCapacity => (int) (2.5 * Math.Pow(Math.E, (double) 20 * CurrentLevel / 33) * 5000);
    }
}