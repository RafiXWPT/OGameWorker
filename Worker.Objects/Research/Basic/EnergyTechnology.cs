using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Basic
{
    public class EnergyTechnology : TechnologyBase
    {
        public EnergyTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.EnergyTechnology;
        public override int BaseMetalCost => 0;
        public override int BaseCrystalCost => 800;
        public override int BaseDeuteriumCost => 400;
    }
}