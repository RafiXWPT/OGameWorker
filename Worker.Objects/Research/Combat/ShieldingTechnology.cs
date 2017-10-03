using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Combat
{
    public class ShieldingTechnology : TechnologyBase
    {
        public ShieldingTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(
            belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.ShieldingTechnology;
        public override int BaseMetalCost => 200;
        public override int BaseCrystalCost => 600;
        public override int BaseDeuteriumCost => 0;
    }
}