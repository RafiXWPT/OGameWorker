using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Combat
{
    public class WeaponTechnology : TechnologyBase
    {
        public WeaponTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.WeaponTechnology;
        public override int BaseMetalCost => 800;
        public override int BaseCrystalCost => 200;
        public override int BaseDeuteriumCost => 0;
    }
}