using System.Linq;
using System.Windows.Media.Imaging;
using OGameWorker.Code;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Structures;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo.DefenseInfo
{
    public class GalaxyPlanetDefenseInfoViewModel
    {
        private readonly EnemyPlanet _planet;
        public bool HasData => _planet.Defenses.Any();

        public BitmapImage RocketLauncherImage => ImageHelper.RocketLauncher;
        public int RocketLauncherAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.RocketLauncher)?.Amount ?? 0;

        public BitmapImage LightLaserImage => ImageHelper.LightLaser;
        public int LightLaserAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.LightLaser)?.Amount ?? 0;

        public BitmapImage HeavyLaserImage => ImageHelper.HeavyLaser;
        public int HeavyLaserAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.HeavyLaser)?.Amount ?? 0;

        public BitmapImage IonCannonImage => ImageHelper.IonCannon;
        public int IonCannonAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.IonCannon)?.Amount ?? 0;

        public BitmapImage GaussCannonImage => ImageHelper.GaussCannon;
        public int GaussCannonAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.GaussCannon)?.Amount ?? 0;

        public BitmapImage PlasmaTurretImage => ImageHelper.PlasmaTurret;
        public int PlasmaTurretAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.PlasmaTurret)?.Amount ?? 0;

        public BitmapImage SmallShieldImage => ImageHelper.SmallShield;
        public int SmallShieldAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.SmallShield)?.Amount ?? 0;

        public BitmapImage LargeShieldImage => ImageHelper.LargeShield;
        public int LargeShieldAmount => _planet.Defenses.FirstOrDefault(d => d.Type == DefenseType.LargeShield)?.Amount ?? 0;
        
        public GalaxyPlanetDefenseInfoViewModel() { }

        public GalaxyPlanetDefenseInfoViewModel(EnemyPlanet planet)
        {
            _planet = planet;
        }
    }
}
