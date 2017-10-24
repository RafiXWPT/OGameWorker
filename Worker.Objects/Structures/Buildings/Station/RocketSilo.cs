using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Buildings.Station
{
    public class RocketSilo : BuildingBase
    {
        public RocketSilo(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType Type => BuildingType.RocketSilo;
        public override int BaseMetalCost => 20000;
        public override int BaseCrystalCost => 20000;
        public override int BaseDeuteriumCost => 1000;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 2.0;
    }
}