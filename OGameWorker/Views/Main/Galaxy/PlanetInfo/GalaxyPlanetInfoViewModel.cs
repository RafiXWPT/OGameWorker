using Worker.Objects.Galaxy.Planet;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo
{
    public class GalaxyPlanetInfoViewModel
    {
        private readonly EnemyPlanet _planet;

        public GalaxyPlanetInfoViewModel(EnemyPlanet planet)
        {
            _planet = planet;
        }
    }
}
