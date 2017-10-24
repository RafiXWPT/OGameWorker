using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Advanced
{
    public class ComputerTechnology : TechnologyBase
    {
        public ComputerTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.ComputerTechnology;
        public override int BaseMetalCost => 0;
        public override int BaseCrystalCost => 400;
        public override int BaseDeuteriumCost => 600;
    }
}