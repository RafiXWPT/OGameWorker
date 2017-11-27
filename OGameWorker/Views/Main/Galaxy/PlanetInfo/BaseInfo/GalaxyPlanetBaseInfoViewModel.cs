using System.Windows.Media.Imaging;
using OGameWorker.Code;
using Worker.Objects.Galaxy.Planet;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo.BaseInfo
{
    public class GalaxyPlanetBaseInfoViewModel
    {
        private readonly EnemyPlanet _planet;

        public string PlayerName => _planet.PlayerName;
        public string PlanetName => _planet.Name;
        public string PlanetPosition => _planet.Position.ToString();

        public BitmapImage MetalImage => ImageHelper.Metal;
        public int MetalAmount => _planet.Metal.Amount;

        public BitmapImage CrystalImage => ImageHelper.Crystal;
        public int CrystalAmount => _planet.Crystal.Amount;

        public BitmapImage DeuteriumImage => ImageHelper.Deuterium;
        public int DeuteriumAmount => _planet.Deuterium.Amount;

        public GalaxyPlanetBaseInfoViewModel() { }

        public GalaxyPlanetBaseInfoViewModel(EnemyPlanet planet)
        {
            _planet = planet;
        }
    }
}
