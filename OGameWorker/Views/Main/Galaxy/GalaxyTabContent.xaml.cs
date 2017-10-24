using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Worker.Objects.Galaxy;

namespace OGameWorker.Views.Main.Galaxy
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
    }
}
