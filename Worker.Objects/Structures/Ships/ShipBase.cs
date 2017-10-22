using System;
using System.Collections.Generic;
using System.Configuration;
using Worker.Objects.Galaxy;
using Worker.Objects.Helpers;

namespace Worker.Objects.Structures.Ships
{
    public abstract class ShipBase : StructureBase
    {
        protected ShipBase(Planet planet, int quantity, bool techReached, bool canBuild) : base(planet, canBuild, techReached)
        {
            Quantity = quantity;
        }

        public abstract ShipAssignment ShipAssignment { get; }
        public abstract ShipType ShipType { get; }
        public abstract int Capacity { get; }
        public abstract int FuelConsumption { get; }
        public abstract int StructuralIntegrity { get; }
        public int Quantity { get; set; }

        public override TimeSpan CreatingTimeDuration => TimeSpan.FromHours(
            StructuralIntegrity / (UniverseSpeed * 2500 *
                                   (1 + PlanetCoreBuildingsHelper.GetPlanetShipyard(BelongsTo).CurrentLevel) *
                                   Math.Pow(2, PlanetCoreBuildingsHelper.GetPlanetNaniteFactory(BelongsTo).CurrentLevel)));
    }
}