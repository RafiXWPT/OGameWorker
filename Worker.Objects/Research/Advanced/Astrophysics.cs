﻿using Worker.Objects.Galaxy;

namespace Worker.Objects.Research.Advanced
{
    public class Astrophysics : TechnologyBase
    {
        public Astrophysics(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override TechnologyType TechnologyType => TechnologyType.Astrophysics;
        public override int BaseMetalCost => 4000;
        public override int BaseCrystalCost => 8000;
        public override int BaseDeuteriumCost => 4000;
    }
}