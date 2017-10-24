using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Drive
{
    public class CombustionDrive : TechnologyBase
    {
        public CombustionDrive(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.CombusionDrive;
        public override int BaseMetalCost => 400;
        public override int BaseCrystalCost => 0;
        public override int BaseDeuteriumCost => 600;
    }
}