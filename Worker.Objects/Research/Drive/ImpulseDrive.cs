using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Drive
{
    public class ImpulseDrive : TechnologyBase
    {
        public ImpulseDrive(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.ImpulseDrive;
        public override int BaseMetalCost => 2000;
        public override int BaseCrystalCost => 4000;
        public override int BaseDeuteriumCost => 600;
    }
}
