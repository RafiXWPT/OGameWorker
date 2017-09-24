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
using Worker.HttpModule.RequestBuilder;
using Worker.Objects;
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
            var requestBuilder = new OGameWorkerRequestBuilder(client);
            var login = client.SendHttpRequest(requestBuilder.BuildLoginRequest("mail.rafixwpt@gmail.com", "raf109aello"));
            if (login.StatusCode == HttpStatusCode.OK)
            {
                var initialView = client.SendHttpRequest(requestBuilder.BuildOverviewRequest());
                ObjectContainer.Instance.PlayerPlanets = await new PlanetsParser().GetPlayerPlanets(initialView.ResponseHtmlDocument);
                ObjectContainer.Instance.PlayerBuildings.Clear();
                foreach (var planet in ObjectContainer.Instance.PlayerPlanets)
                {
                    var planetResources = client.SendHttpRequest(requestBuilder.BuildResourceRequest(planet.Id));
                    var planetResourceBuildings = await new BuildingsParser().GetResourceBuildings(planetResources.ResponseHtmlDocument, planet);
                    var planetStation = client.SendHttpRequest(requestBuilder.BuildStationRequest(planet.Id));
                    var planetStationBuildings = await new BuildingsParser().GetStationBuildings(planetStation.ResponseHtmlDocument, planet);
                    ObjectContainer.Instance.PlayerBuildings.AddRange(planetResourceBuildings);
                    ObjectContainer.Instance.PlayerBuildings.AddRange(planetStationBuildings);
                }
            }
        }
    }
}
