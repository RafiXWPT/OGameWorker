using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Drive
{
    public class ImpulseDrive : TechnologyBase
    {
        public ImpulseDrive(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.ImpulseDrive;
        public override int BaseMetalCost => 2000;
        public override int BaseCrystalCost => 4000;
        public override int BaseDeuteriumCost => 600;
    }
}