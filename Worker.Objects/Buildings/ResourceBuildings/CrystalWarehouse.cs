﻿using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.ResourceBuildings
{
    public class CrystalWarehouse : BuildingBase
    {
        public CrystalWarehouse(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.CrystalWarehouse;
    }
}
