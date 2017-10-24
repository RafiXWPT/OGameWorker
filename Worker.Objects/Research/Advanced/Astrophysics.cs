using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Advanced
{
    public class Astrophysics : TechnologyBase
    {
        public Astrophysics(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.Astrophysics;
        public override int BaseMetalCost => 4000;
        public override int BaseCrystalCost => 8000;
        public override int BaseDeuteriumCost => 4000;
    }
}