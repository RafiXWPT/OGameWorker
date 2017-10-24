using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Defenses.PassiveDefense
{
    public class SmallShield : DefenseBase
    {
        public override DefenseType Type => DefenseType.SmallShield;
        public override int MetalCost => 10000;
        public override int CrystalCost => 10000;
        public override int DeuteriumCost => 0;

        public SmallShield(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
