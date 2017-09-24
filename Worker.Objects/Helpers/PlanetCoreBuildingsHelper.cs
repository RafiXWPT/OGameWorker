using System.Linq;
using Worker.Objects.Buildings;
using Worker.Objects.Buildings.Station;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Helpers
{
    public static class PlanetCoreBuildingsHelper
    {
        public static Shipyard GetPlanetShipyard(Planet planet)
        {
            return ObjectContainer.Instance.PlayerBuildings.FirstOrDefault(b => b.BelongsTo.Id == planet.Id && b.BuildingType == BuildingType.Shipyard) as Shipyard;
        }

        public static RoboticsFactory GetPlanetRoboticsFactory(Planet planet)
        {
            return ObjectContainer.Instance.PlayerBuildings.FirstOrDefault(b => b.BelongsTo.Id == planet.Id && b.BuildingType == BuildingType.RoboticsFactory) as RoboticsFactory;
        }
    }
}
