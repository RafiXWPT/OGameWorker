using Worker.Objects.Structures;
using System.Linq;
using System.Windows.Media.Imaging;
using OGameWorker.Code;
using Worker.Objects.Galaxy.Planet;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo.BuildingsInfo
{
    public class GalaxyPlanetBuildingsInfoViewModel
    {
        private readonly EnemyPlanet _planet;
        public bool HasData => _planet.Buildings.Any();

        public BitmapImage MetalMineImage => ImageHelper.MetalMine;
        public int MetalMineLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.MetalMine)?.CurrentLevel ?? 0;

        public BitmapImage CrystalMineImage => ImageHelper.CrystalMine;
        public int CrystalMineLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.CrystalMine)?.CurrentLevel ?? 0;

        public BitmapImage DeuteriumExtractorImage => ImageHelper.DeuteriumExtractor;
        public int DeuteriumExtractorLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.DeuteriumExtractor)?.CurrentLevel ?? 0;

        public BitmapImage SolarPowerplantImage => ImageHelper.SolarPowerplant;
        public int SolarPowerplantLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.SolarPowerPlant)?.CurrentLevel ?? 0;

        public BitmapImage FusionReactorImage => ImageHelper.FusionReactor;
        public int FusionReactorLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.FusionReactor)?.CurrentLevel ?? 0;

        public BitmapImage MetalStorageImage => ImageHelper.MetalStorage;
        public int MetalStorageLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.MetalStorage)?.CurrentLevel ?? 0;

        public BitmapImage CrystalStorageImage => ImageHelper.CrystalStorage;
        public int CrystalStorageLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.CrystalStorage)?.CurrentLevel ?? 0;

        public BitmapImage DeuteriumTankImage => ImageHelper.DeuteriumTank;
        public int DeuteriumTankLevel => _planet.Buildings.FirstOrDefault(b => b.Type == BuildingType.DeuteriumTank)?.CurrentLevel ?? 0;
        
        public GalaxyPlanetBuildingsInfoViewModel() { }

        public GalaxyPlanetBuildingsInfoViewModel(EnemyPlanet planet)
        {
            _planet = planet;
        }
    }
}
