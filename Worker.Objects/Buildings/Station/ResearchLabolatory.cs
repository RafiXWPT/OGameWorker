using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.Station
{
    public class ResearchLabolatory : BuildingBase
    {
        public ResearchLabolatory(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.ResearchLabolatory;
    }
}
