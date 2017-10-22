using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Defenses.PassiveDefense
{
    public class LargeShield : DefenseBase
    {
        public override DefenseType Type => DefenseType.LargeShield;
        public override int MetalCost => 50000;
        public override int CrystalCost => 50000;
        public override int DeuteriumCost => 0;

        public LargeShield(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
