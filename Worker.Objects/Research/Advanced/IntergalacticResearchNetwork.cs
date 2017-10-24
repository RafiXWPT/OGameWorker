using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Advanced
{
    public class IntergalacticResearchNetwork : TechnologyBase
    {
        public IntergalacticResearchNetwork(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(
            belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.IntergalacticResearchNetwork;
        public override int BaseMetalCost => 240000;
        public override int BaseCrystalCost => 400000;
        public override int BaseDeuteriumCost => 160000;
    }
}