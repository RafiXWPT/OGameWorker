using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Defenses.ActiveDefense
{
    public class RocketLauncher : DefenseBase
    {
        public override DefenseType Type => DefenseType.RocketLauncher;
        public override int MetalCost => 2000;
        public override int CrystalCost => 0;
        public override int DeuteriumCost => 0;

        public RocketLauncher(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
