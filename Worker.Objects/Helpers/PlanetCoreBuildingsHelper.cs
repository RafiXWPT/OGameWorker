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
            return ObjectContainer.Instance.GetBuilding(planet, BuildingType.Shipyard) as Shipyard;
        }

        public static RoboticsFactory GetPlanetRoboticsFactory(Planet planet)
        {
            return ObjectContainer.Instance.GetBuilding(planet, BuildingType.RoboticsFactory) as RoboticsFactory;
        }

        public static ResearchLabolatory GetPlanetResearchLabolatory(Planet planet)
        {
            return ObjectContainer.Instance.GetBuilding(planet, BuildingType.ResearchLabolatory) as ResearchLabolatory;
        }
    }
}
