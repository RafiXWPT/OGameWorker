using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Buildings
{
    public abstract class BuildingBase
    {
        public Planet BelongsTo { get; set; }
        public int CurrentLevel { get; set; }
        public bool CanBuild { get; set; }
        public double MetalCost { get; set; }
        public double CrystalCost { get; set; }
        public double DeuteriumCost { get; set; }
        public TimeSpan UpgradeTimeDuration { get; set; }

        protected BuildingBase(Planet belongsTo, int currentLevel)
        {
            BelongsTo = belongsTo;
            CurrentLevel = currentLevel;
        }
    }
}
