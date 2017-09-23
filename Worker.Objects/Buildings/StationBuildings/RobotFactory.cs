using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.StationBuildings
{
    public class RobotFactory : BuildingBase
    {
        public RobotFactory(Planet belongsTo, int currentLevel, bool techReached) : base(belongsTo, currentLevel, techReached)
        {
        }

        public override BuildingType BuildingType => BuildingType.RobotFactory;
    }
}
