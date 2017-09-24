using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Warehouse
{
    public class DeuteriumWarehouse : BuildingBase
    {
        public DeuteriumWarehouse(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.DeuteriumWarehouse;
    }
}
