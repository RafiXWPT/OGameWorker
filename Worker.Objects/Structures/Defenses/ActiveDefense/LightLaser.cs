using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Defenses.ActiveDefense
{
    public class LightLaser : DefenseBase
    {
        public override DefenseType Type => DefenseType.LightLaser;
        public override int MetalCost => 1500;
        public override int CrystalCost => 500;
        public override int DeuteriumCost => 0;

        public LightLaser(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, amount, techReached, canBuild)
        {
        }
    }
}
