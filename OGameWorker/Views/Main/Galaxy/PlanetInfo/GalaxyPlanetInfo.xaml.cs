using System.Windows;
using Worker.Objects.Galaxy.Planet;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo
{
    /// <summary>
    /// Interaction logic for GalaxyPlanetInfo.xaml
    /// </summary>
    public partial class GalaxyPlanetInfo : Window
    {
        public GalaxyPlanetInfoViewModel ViewModel { get; set; }
        public GalaxyPlanetInfo(EnemyPlanet planet)
        {
            ViewModel = new GalaxyPlanetInfoViewModel(planet);
            InitializeComponent();
        }
    }
}
