using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.ResourceBuildings
{
    public class DeuteriumWarehouse : BuildingBase
    {
        public DeuteriumWarehouse(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.DeuteriumWarehouse;
    }
}
