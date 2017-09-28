using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Basic
{
    public class PlasmaTechnology : TechnologyBase
    {
        public PlasmaTechnology(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.PlasmaTechnology;
        public override int BaseMetalCost => 2000;
        public override int BaseCrystalCost => 4000;
        public override int BaseDeuteriumCost => 1000;
    }
}
