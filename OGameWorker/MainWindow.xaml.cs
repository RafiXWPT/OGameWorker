using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using HtmlAgilityPack;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.HttpModule.RequestBuilder;
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;
using Worker.Objects.Ships;
using Worker.Objects.Ships.Fighter;
using Worker.Parser.Buildings;
using Worker.Parser.Planets;
using Worker.Parser.Resources;

namespace OGameWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new OGameHttpClient("s147-pl.ogame.gameforge.com");
            var loginMethod = await client.LogIn();
            if (loginMethod.StatusCode == HttpStatusCode.OK)
            {
                await client.RefreshObjectContainer();
                await client.RefreshMissions();
            }

            //var result = new OGameFleetSender(client, new Planet("", new Planet.PlanetPosition {Galaxy = 4, System = 105, Planet = 4}), FleetSpeed.Speed100, MissionType.Attack, new List<ShipBase> {new LightFighter()} ).SendFleet();
            var oc = ObjectContainer.Instance;
        }
    }
}
