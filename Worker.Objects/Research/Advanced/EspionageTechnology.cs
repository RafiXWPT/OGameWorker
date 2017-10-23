using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Advanced
{
    public class EspionageTechnology : TechnologyBase
    {
        public EspionageTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(
            belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.EspionageTechnology;
        public override int BaseMetalCost => 200;
        public override int BaseCrystalCost => 1000;
        public override int BaseDeuteriumCost => 200;
    }
}