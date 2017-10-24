using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Drive
{
    public class HyperspaceDrive : TechnologyBase
    {
        public HyperspaceDrive(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.HyperspaceDrive;
        public override int BaseMetalCost => 10000;
        public override int BaseCrystalCost => 20000;
        public override int BaseDeuteriumCost => 6000;
    }
}