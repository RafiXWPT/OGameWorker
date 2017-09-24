using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Station
{
    public class RobotFactory : BuildingBase
    {
        public RobotFactory(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.RobotFactory;
    }
}
