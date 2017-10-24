using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Worker.Objects.Galaxy;
using Worker.Objects.Helpers;

namespace Worker.Objects.Structures.Buildings
{
    public abstract class BuildingBase : StructureBase
    {
        public abstract BuildingType Type { get; }
        public abstract int BaseMetalCost { get; }
        public abstract int BaseCrystalCost { get; }
        public abstract int BaseDeuteriumCost { get; }
        public abstract int EnergyConsumption { get; }
        public abstract double CostIncreaseFactor { get; }

        public int CurrentLevel { get; set; }

        public override int MetalCost => (int) (BaseMetalCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public override int CrystalCost => (int) (BaseCrystalCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public override int DeuteriumCost => (int) (BaseDeuteriumCost * Math.Pow(CostIncreaseFactor, CurrentLevel));

        public override TimeSpan CreatingTimeDuration => ObjectContainer.Instance.PlayerBuildings.Any()
            ? TimeSpan.FromHours((MetalCost + CrystalCost) /
                                 (UniverseSpeed * 2500 *
                                  (1 + PlanetCoreBuildingsHelper.GetPlanetRoboticsFactory(BelongsTo).CurrentLevel) *
                                  Math.Pow(2,
                                      PlanetCoreBuildingsHelper.GetPlanetNaniteFactory(BelongsTo).CurrentLevel)))
            : TimeSpan.MaxValue;

        protected BuildingBase(Planet belongsTo, int currentLevel, bool techReached, bool canBuild) : base(belongsTo, canBuild, techReached)
        {
            CurrentLevel = currentLevel;
        }
    }
}