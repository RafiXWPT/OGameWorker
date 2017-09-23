using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.ResourceBuildings
{
    public class FusionPowerPlant : BuildingBase
    {
        public FusionPowerPlant(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.FusionPowerPlant;
    }
}
