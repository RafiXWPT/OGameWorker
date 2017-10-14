using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            var planet = ((e.Source as RadButton).DataContext as GalaxyPlanetInfo);
            await (DataContext as GalaxyViewModel).SendEspionageProbesTask(planet);
        }
    }
}
