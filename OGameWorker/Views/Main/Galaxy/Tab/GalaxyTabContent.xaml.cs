using OGameWorker.Views.Main.Galaxy.PlanetInfo;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Worker.Objects.Galaxy.Planet;

namespace OGameWorker.Views.Main.Galaxy.Tab
{
    /// <summary>
    /// Interaction logic for GalaxyTab.xaml
    /// </summary>
    public partial class GalaxyTabContent : UserControl
    {
        public GalaxyTabContent()
        {
            InitializeComponent();
        }

        private async void SendEspionageProbesClick(object sender, RoutedEventArgs e)
        {
            var planet = (e.Source as RadButton).DataContext as Planet;
            await (DataContext as GalaxyViewModel).SendEspionageProbesTask(planet);
        }

        private void OpenPlanetInfo(object sender, MouseButtonEventArgs e)
        {
            var planet = GetPlanetFromRowDoubleClick(e);
            if (planet == null)
                return;

            var galaxyPlanetInfo = new GalaxyPlanetInfo(planet);
            galaxyPlanetInfo.ShowDialog();
        }

        private EnemyPlanet GetPlanetFromRowDoubleClick(MouseButtonEventArgs e)
        {
            var planet = ((e.OriginalSource as FrameworkElement)?.ParentOfType<GridViewRow>()?.DataContext) as Planet;
            if (planet == null)
                return null;

            if (planet.Type == PlanetType.Self || planet.Type == PlanetType.Empty)
                return null;

            var enemyPlanet = (EnemyPlanet) planet;

            return enemyPlanet.ScanStatus != ScanStatus.Scanned ? null : enemyPlanet;
        }
    }
}
