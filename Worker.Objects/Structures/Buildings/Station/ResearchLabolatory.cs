using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Buildings.Station
{
    public class ResearchLabolatory : BuildingBase
    {
        public ResearchLabolatory(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType BuildingType => BuildingType.ResearchLabolatory;
        public override int BaseMetalCost => 200;
        public override int BaseCrystalCost => 400;
        public override int BaseDeuteriumCost => 200;
        public override int EnergyConsumption => 0;
        public override double CostIncreaseFactor => 2.0;
    }
}