using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace OGameWorker.Code
{
    public static class ImageHelper
    {
        private static BitmapImage GetImage(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                memoryStream.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public static BitmapImage Metal => GetImage(Resources.Resources.Metal);
        public static BitmapImage Crystal => GetImage(Resources.Resources.Crystal);
        public static BitmapImage Deuterium => GetImage(Resources.Resources.Deuterium);

        public static BitmapImage MetalMine => GetImage(Resources.Resources.MetalMine);
        public static BitmapImage CrystalMine => GetImage(Resources.Resources.CrystalMine);
        public static BitmapImage DeuteriumExtractor => GetImage(Resources.Resources.DeuteriumExtractor);
        public static BitmapImage SolarPowerplant => GetImage(Resources.Resources.SolarPowerPlant);
        public static BitmapImage FusionReactor => GetImage(Resources.Resources.FusionReactor);
        public static BitmapImage MetalStorage => GetImage(Resources.Resources.MetalStorage);
        public static BitmapImage CrystalStorage => GetImage(Resources.Resources.CrystalStorage);
        public static BitmapImage DeuteriumTank => GetImage(Resources.Resources.DeuteriumTank);

        public static BitmapImage Shipyard => GetImage(Resources.Resources.Shipyard);
        public static BitmapImage RoboticsFactory => GetImage(Resources.Resources.RoboticsFactory);
        public static BitmapImage ResearchLab => GetImage(Resources.Resources.ResearchLab);
        public static BitmapImage AllianceDepot => GetImage(Resources.Resources.AllianceDepot);
        public static BitmapImage MissileSilo => GetImage(Resources.Resources.MissileSilo);
        public static BitmapImage NaniteFactory => GetImage(Resources.Resources.NaniteFactory);
        public static BitmapImage Terraformer => GetImage(Resources.Resources.Terraformer);

        public static BitmapImage SmallCargo => GetImage(Resources.Resources.SmallCargo);
        public static BitmapImage LargeCargo => GetImage(Resources.Resources.LargeCargo);
        public static BitmapImage LightFighter => GetImage(Resources.Resources.LightFighter);
        public static BitmapImage HeavyFighter => GetImage(Resources.Resources.HeavyFighter);
        public static BitmapImage Cruiser => GetImage(Resources.Resources.Cruiser);
        public static BitmapImage Battleship => GetImage(Resources.Resources.BattleShip);
        public static BitmapImage Battlecruiser => GetImage(Resources.Resources.Battlecruiser);
        public static BitmapImage Bomber => GetImage(Resources.Resources.Bomber);
        public static BitmapImage Destroyer => GetImage(Resources.Resources.Destroyer);
        public static BitmapImage Deathstar => GetImage(Resources.Resources.DeathStar);
        public static BitmapImage Recycler => GetImage(Resources.Resources.Recycler);
        public static BitmapImage ColonyShip => GetImage(Resources.Resources.ColonyShip);
        public static BitmapImage EspionageProbe => GetImage(Resources.Resources.EspionageProbe);
        public static BitmapImage SolarSatellite => GetImage(Resources.Resources.SolarSatellite);

        public static BitmapImage EnergyTechnology => GetImage(Resources.Resources.EnergyTechnology);
        public static BitmapImage LaserTechnology => GetImage(Resources.Resources.LaserTechnology);
        public static BitmapImage IonTechnology => GetImage(Resources.Resources.IonTechnology);
        public static BitmapImage PlasmaTechnology => GetImage(Resources.Resources.PlasmaTechnology);
        public static BitmapImage HyperspaceTechnology => GetImage(Resources.Resources.HyperspaceTechnology);
        public static BitmapImage CombustionDrive => GetImage(Resources.Resources.CombustionDrive);
        public static BitmapImage ImpulseDrive => GetImage(Resources.Resources.ImpulseDrive);
        public static BitmapImage HyperspaceDrive => GetImage(Resources.Resources.HyperspaceDrive);
        public static BitmapImage EspionageTechnology => GetImage(Resources.Resources.EnergyTechnology);
        public static BitmapImage ComputerTechnology => GetImage(Resources.Resources.ComputerTechnology);
        public static BitmapImage Astrophysics => GetImage(Resources.Resources.Astrophysics);
        public static BitmapImage IntergalacticResearchNetwork => GetImage(Resources.Resources.IntergalacticResearchNetwork);
        public static BitmapImage GravitonTechnology => GetImage(Resources.Resources.GravitonTechnology);
        public static BitmapImage WeaponTechnology => GetImage(Resources.Resources.WeaponTechnology);
        public static BitmapImage ShieldingTechnology => GetImage(Resources.Resources.ShieldingTechnology);
        public static BitmapImage ArmourTechnology => GetImage(Resources.Resources.ArmourTechnology);

        public static BitmapImage RocketLauncher => GetImage(Resources.Resources.RocketLauncher);
        public static BitmapImage LightLaser => GetImage(Resources.Resources.LightLaser);
        public static BitmapImage HeavyLaser => GetImage(Resources.Resources.HeavyLaser);
        public static BitmapImage IonCannon => GetImage(Resources.Resources.IonCannon);
        public static BitmapImage GaussCannon => GetImage(Resources.Resources.GaussCannon);
        public static BitmapImage PlasmaTurret => GetImage(Resources.Resources.PlasmaTurret);
        public static BitmapImage SmallShield => GetImage(Resources.Resources.SmallShield);
        public static BitmapImage LargeShield => GetImage(Resources.Resources.LargeShield);
    }
}