using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Basic
{
    public class HyperspaceTechnology : TechnologyBase
    {
        public HyperspaceTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(
            belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.HyperspaceTechnology;
        public override int BaseMetalCost => 0;
        public override int BaseCrystalCost => 4000;
        public override int BaseDeuteriumCost => 2000;
    }
}