using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Basic
{
    public class IonTechnology : TechnologyBase
    {
        public IonTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.IonTechnology;
        public override int BaseMetalCost => 1000;
        public override int BaseCrystalCost => 300;
        public override int BaseDeuteriumCost => 100;
    }
}