using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OGameWorker.Code;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.Objects;
using Worker.Objects.Galaxy;

namespace OGameWorker.Views.Main.Galaxy
{
    public class GalaxyViewModel : OGameWorkerBaseViewModel
    {
        public bool IsBusy { get; set; }
        private readonly OGameHttpClient _client;
        public ObservableCollection<GalaxyPlanetInfo> Planets { get; set; } = new ObservableCollection<GalaxyPlanetInfo>();
        public int Galaxy { get; set; }
        public int System { get; set; }
        public int Span { get; set; }

        public ReactiveCommand LoadGalaxyDataCommand { get; protected set; }
        //public ReactiveCommand SendEspionageProbesCommand { get; protected set; }

        public GalaxyViewModel()
        {
            
        }

        public GalaxyViewModel(OGameHttpClient client)
        {
            LoadGalaxyDataCommand = ReactiveCommand.CreateFromTask(t => LoadGalaxyDataTask(Galaxy, System, Span));
            //SendEspionageProbesCommand = ReactiveCommand.CreateFromTask(t => SendEspionageProbesTask());
            _client = client;
        }

        public async Task LoadGalaxyDataTask(int galaxy, int startSystem, int span)
        {
            await Task.Run(async () =>
            {
                IsBusy = true;

                var fromSystem = startSystem - span;
                var toSystem = startSystem + span;
                var inMemoryPlanets = new List<GalaxyPlanetInfo>();
                for (var i = fromSystem; i < toSystem; i++)
                {
                    var galaxyPlanets = await _client.GetGalaxyView(galaxy, i);
                    inMemoryPlanets.AddRange(galaxyPlanets);
                }

                RunOnDispatcher(() =>
                {
                    Planets.Clear();
                    foreach (var planet in inMemoryPlanets)
                    {
                        Planets.Add(planet);
                    }
                });

                IsBusy = false;
            });
        }

        public void SendAttack(GalaxyPlanetInfo info)
        {
            
        }

        public void SendTransport(GalaxyPlanetInfo info)
        {
            
        }

        public async Task SendEspionageProbesTask(GalaxyPlanetInfo info)
        {
            await OGameFleetSender.Espionage(_client, ObjectContainer.Instance.CurrentSelectedPlanet,
                    Planet.FromGalaxyPlanetInfo(info))
                .SendFleet();
        }
    }
}
