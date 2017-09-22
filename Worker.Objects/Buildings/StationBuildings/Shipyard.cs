using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings.StationBuildings
{
    public class Shipyard : BuildingBase
    {
        public Shipyard(Planet belongsTo, int currentLevel) : base(belongsTo, currentLevel)
        {
        }
    }
}
