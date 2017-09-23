using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.ResourceBuildings
{
    public class DeuteriumExtractor : BuildingBase
    {
        public DeuteriumExtractor(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.DeuteriumExtractor;
    }
}
