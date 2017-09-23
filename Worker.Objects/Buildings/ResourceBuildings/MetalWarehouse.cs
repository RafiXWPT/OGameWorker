using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.ResourceBuildings
{
    public class MetalWarehouse : BuildingBase
    {
        public MetalWarehouse(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.MetalWarehouse;
    }
}
