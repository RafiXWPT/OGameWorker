using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Defenses.ActiveDefense
{
    public class HeavyLaser : DefenseBase
    {
        public override DefenseType Type => DefenseType.HeavyLaser;
        public override int MetalCost => 6000;
        public override int CrystalCost => 2000;
        public override int DeuteriumCost => 0;

        public HeavyLaser(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
