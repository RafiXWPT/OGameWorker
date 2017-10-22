using Worker.Objects.Galaxy;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Buildings.Station;

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

        public static NaniteFactory GetPlanetNaniteFactory(Planet planet)
        {
            return ObjectContainer.Instance.GetBuilding(planet, BuildingType.NaniteFactory) as NaniteFactory;
        }
    }
}