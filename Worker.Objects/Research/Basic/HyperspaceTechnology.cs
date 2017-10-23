using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Basic
{
    public class HyperspaceTechnology : TechnologyBase
    {
        public HyperspaceTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(
            belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.HyperspaceTechnology;
        public override int BaseMetalCost => 0;
        public override int BaseCrystalCost => 4000;
        public override int BaseDeuteriumCost => 2000;
    }
}