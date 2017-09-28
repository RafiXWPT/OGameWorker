using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Combat
{
    public class ArmourTechnology : TechnologyBase
    {
        public ArmourTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.ArmourTechnology;
        public override int BaseMetalCost => 1000;
        public override int BaseCrystalCost => 0;
        public override int BaseDeuteriumCost => 0;
    }
}
