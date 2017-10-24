using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Buildings.Station
{
    public class RoboticsFactory : BuildingBase
    {
        public RoboticsFactory(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType Type => BuildingType.RoboticsFactory;
        public override int BaseMetalCost => 400;
        public override int BaseCrystalCost => 120;
        public override int BaseDeuteriumCost => 200;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 2.0;
    }
}