﻿using System;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;

namespace Worker.Objects.Structures.Buildings.Resource
{
    public class DeuteriumExtractor : BuildingBase
    {
        public DeuteriumExtractor(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo,
            currentLevel, techReached, canBuild)
        {
        }

        public override BuildingType Type => BuildingType.DeuteriumExtractor;
        public override int BaseMetalCost => 225;
        public override int BaseCrystalCost => 75;
        public override int BaseDeuteriumCost => 0;
        public override int EnergyConsumption => (int) (20 * CurrentLevel * Math.Pow(1.1, CurrentLevel));
        public override double CostIncreaseFactor => 1.5;

        public int DeuteriumProduction => (int) (UniverseSpeed * 10 * CurrentLevel * Math.Pow(1.1, CurrentLevel) * (1.44 - 0.004 * (BelongsTo as PlayerPlanet).Temperature.Max));
    }
}