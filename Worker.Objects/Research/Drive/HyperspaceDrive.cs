using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Drive
{
    public class HyperspaceDrive : TechnologyBase
    {
        public HyperspaceDrive(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.HyperspaceDrive;
        public override int BaseMetalCost => 10000;
        public override int BaseCrystalCost => 20000;
        public override int BaseDeuteriumCost => 6000;
    }
}
