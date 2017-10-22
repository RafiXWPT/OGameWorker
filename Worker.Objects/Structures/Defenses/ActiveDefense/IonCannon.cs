using Worker.Objects.Galaxy;

namespace Worker.Objects.Structures.Defenses.ActiveDefense
{
    public class IonCannon : DefenseBase
    {

        public override DefenseType Type => DefenseType.IonCannon;
        public override int MetalCost => 2000;
        public override int CrystalCost => 6000;
        public override int DeuteriumCost => 0;

        public IonCannon(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
