using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Advanced
{
    public class EspionageTechnology : TechnologyBase
    {
        public EspionageTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.EspionageTechnology;
        public override int BaseMetalCost => 200;
        public override int BaseCrystalCost => 1000;
        public override int BaseDeuteriumCost => 200;
    }
}
