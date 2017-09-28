using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Advanced
{
    public class GravitonTechnology : TechnologyBase
    {
        public GravitonTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.GravitonTechnology;
        public override int BaseMetalCost => 0;
        public override int BaseCrystalCost => 0;
        public override int BaseDeuteriumCost => 0;
    }
}
