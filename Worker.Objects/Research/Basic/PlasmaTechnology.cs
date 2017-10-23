using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Basic
{
    public class PlasmaTechnology : TechnologyBase
    {
        public PlasmaTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.PlasmaTechnology;
        public override int BaseMetalCost => 2000;
        public override int BaseCrystalCost => 4000;
        public override int BaseDeuteriumCost => 1000;
    }
}