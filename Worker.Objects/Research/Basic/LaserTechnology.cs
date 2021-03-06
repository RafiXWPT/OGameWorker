﻿using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Basic
{
    public class LaserTechnology : TechnologyBase
    {
        public LaserTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.LaserTechnology;
        public override int BaseMetalCost => 200;
        public override int BaseCrystalCost => 100;
        public override int BaseDeuteriumCost => 0;
    }
}