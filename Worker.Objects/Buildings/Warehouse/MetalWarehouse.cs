using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Warehouse
{
    public class MetalWarehouse : BuildingBase
    {
        public MetalWarehouse(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.MetalWarehouse;
    }
}
