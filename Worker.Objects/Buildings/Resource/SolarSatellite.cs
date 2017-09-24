using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Resource
{
    public class SolarSatellite : BuildingBase
    {
        public SolarSatellite(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.SolarSatellite;
    }
}
