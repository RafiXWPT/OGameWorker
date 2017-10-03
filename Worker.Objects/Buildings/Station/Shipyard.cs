using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Station
{
    public class Shipyard : BuildingBase
    {
        public Shipyard(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.Shipyard;
        public override int BaseMetalCost => 400;
        public override int BaseCrystalCost => 200;
        public override int BaseDeuteriumCost => 100;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 2.0;
    }
}