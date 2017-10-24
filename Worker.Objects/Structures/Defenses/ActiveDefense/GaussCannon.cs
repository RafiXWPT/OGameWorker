using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Defenses.ActiveDefense
{
    public class GaussCannon : DefenseBase
    {
        public override DefenseType Type => DefenseType.GaussCannon;
        public override int MetalCost => 20000;
        public override int CrystalCost => 15000;
        public override int DeuteriumCost => 2000;

        public GaussCannon(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
