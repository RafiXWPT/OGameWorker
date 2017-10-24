using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Defenses.ActiveDefense
{
    public class PlasmaTurret : DefenseBase
    {
        public override DefenseType Type => DefenseType.PlasmaTurret;
        public override int MetalCost => 50000;
        public override int CrystalCost => 50000;
        public override int DeuteriumCost => 30000;

        public PlasmaTurret(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
