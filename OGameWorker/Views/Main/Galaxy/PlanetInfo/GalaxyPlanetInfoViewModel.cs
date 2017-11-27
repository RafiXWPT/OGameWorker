using System.Windows.Media.Imaging;
using OGameWorker.Code;
using OGameWorker.Views.Main.Galaxy.PlanetInfo.BaseInfo;
using OGameWorker.Views.Main.Galaxy.PlanetInfo.BuildingsInfo;
using OGameWorker.Views.Main.Galaxy.PlanetInfo.DefenseInfo;
using OGameWorker.Views.Main.Galaxy.PlanetInfo.FleetInfo;
using Worker.Objects.Galaxy.Planet;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo
{
    public class GalaxyPlanetInfoViewModel
    {
        public GalaxyPlanetBaseInfoViewModel BaseInfoViewModel { get; }
        public GalaxyPlanetBuildingsInfoViewModel BuildingsInfoViewModel { get; }
        public GalaxyPlanetFleetInfoViewModel FleetInfoViewModel { get; }
        public GalaxyPlanetDefenseInfoViewModel DefenseInfoViewModel { get; }

        public GalaxyPlanetInfoViewModel() { }

        public GalaxyPlanetInfoViewModel(EnemyPlanet planet)
        {
            BaseInfoViewModel = new GalaxyPlanetBaseInfoViewModel(planet);
            BuildingsInfoViewModel = new GalaxyPlanetBuildingsInfoViewModel(planet);
            DefenseInfoViewModel = new GalaxyPlanetDefenseInfoViewModel(planet);
            FleetInfoViewModel = new GalaxyPlanetFleetInfoViewModel(planet);
        }
    }
}
