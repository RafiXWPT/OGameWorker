﻿using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Research.Combat
{
    public class WeaponTechnology : TechnologyBase
    {
        public WeaponTechnology(PlayerPlanet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType Type => TechnologyType.WeaponTechnology;
        public override int BaseMetalCost => 800;
        public override int BaseCrystalCost => 200;
        public override int BaseDeuteriumCost => 0;
    }
}