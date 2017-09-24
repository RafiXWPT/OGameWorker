using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Warehouse
{
    public class CrystalWarehouse : BuildingBase
    {
        public CrystalWarehouse(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.CrystalWarehouse;
    }
}
