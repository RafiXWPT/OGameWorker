using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.ResourceBuildings
{
    public class SolarPowerPlant : BuildingBase
    {
        public SolarPowerPlant(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.SolarPowerPlant;
    }
}
