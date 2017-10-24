using System;
using System.Linq;
using Worker.Objects.Galaxy;
using Worker.Objects.Helpers;

namespace Worker.Objects.Structures.Defenses
{
    public abstract class DefenseBase : StructureBase
    {
        public int Amount { get; }
        public abstract DefenseType Type { get; }

        public override TimeSpan CreatingTimeDuration => ObjectContainer.Instance.PlayerBuildings.Any()
            ? TimeSpan.FromHours((MetalCost + CrystalCost) /
                                 (UniverseSpeed * 2500 *
                                  (1 + PlanetCoreBuildingsHelper.GetPlanetShipyard(BelongsTo).CurrentLevel) *
                                  Math.Pow(2,
                                      PlanetCoreBuildingsHelper.GetPlanetNaniteFactory(BelongsTo).CurrentLevel)))
            : TimeSpan.MaxValue;

        protected DefenseBase(Planet belongsTo, int amount, bool techReached, bool canBuild) : base(belongsTo, canBuild, techReached)
        {
            Amount = amount;
        }
    }
}
