using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Buildings.Station
{
    public class NaniteFactory : BuildingBase
    {
        public NaniteFactory(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.NaniteFactory;
        public override int BaseMetalCost => 1000000;
        public override int BaseCrystalCost => 500000;
        public override int BaseDeuteriumCost => 100000;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 2.0;
    }
}
