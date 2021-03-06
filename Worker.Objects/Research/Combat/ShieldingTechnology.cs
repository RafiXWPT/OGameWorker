﻿using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Combat
{
    public class ShieldingTechnology : TechnologyBase
    {
        public ShieldingTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(
            belongsTo, currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.ShieldingTechnology;
        public override int BaseMetalCost => 200;
        public override int BaseCrystalCost => 600;
        public override int BaseDeuteriumCost => 0;
    }
}