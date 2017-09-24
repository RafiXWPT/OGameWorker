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
                var messageContainer = client.SendHttpRequest(requestBuilder.BuildOverviewRequest());
                var metal = await new ResourceParser().GetMetal(messageContainer.ResponseHtmlDocument);
                var planets = await new PlanetsParser().GetPlayerPlanets(messageContainer.ResponseHtmlDocument);
                var planetResources = client.SendHttpRequest(requestBuilder.BuildResourceRequest(planets.First().Id));
                var planetStation = client.SendHttpRequest(requestBuilder.BuildStationRequest(planets.First().Id));
                var planetResourceBuildings = await new BuildingsParser().GetResourceBuildings(planetResources.ResponseHtmlDocument, planets.First());
                var planetStationBuildings = await new BuildingsParser().GetStationBuildings(planetStation.ResponseHtmlDocument, planets.First());
            }
        }
    }
}
