using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OGameWorker.Code;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.OGameClient;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Messages;
using Worker.Objects.Resources;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Buildings.Resource;

namespace OGameWorker.Views.Main.Resources
{
    public class ResourcesViewModel : OGameWorkerBaseViewModel
    {
        private Metal _metal;
        private Crystal _crystal;
        private Deuterium _deuterium;
        private PlayerPlanet _planet;

        public Metal Metal
        {
            get => _metal;
            set => this.RaiseAndSetIfChanged(ref _metal, value);
        }

        public Crystal Crystal
        {
            get => _crystal;
            set => this.RaiseAndSetIfChanged(ref _crystal, value);
        }

        public Deuterium Deuterium
        {
            get => _deuterium;
            set => this.RaiseAndSetIfChanged(ref _deuterium, value);
        }

        public PlayerPlanet Planet
        {
            get => _planet;
            set => this.RaiseAndSetIfChanged(ref _planet, value);
        }

        public ResourcesViewModel() { }

        public ResourcesViewModel(OGameHttpClient client) : base(client)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(i => ObjectContainer.Instance.Initialized && ObjectContainer.Instance.PlayerPlanets.Any())
                .SubscribeOnDispatcher()
                .Subscribe(token =>
                {
                    UpdateResourcesLive();
                });
        }

        public void Synchronize(PlayerPlanet currentPlanet)
        {
            if (currentPlanet == null)
                return;

            Metal = currentPlanet.Metal;
            Crystal = currentPlanet.Crystal;
            Deuterium = currentPlanet.Deuterium;
        }

        private void UpdateResourcesLive()
        {
            foreach (var planet in ObjectContainer.Instance.PlayerPlanets)
            {
                planet.Metal = new Metal(planet.Metal.Amount + (((MetalMine)ObjectContainer.Instance.GetBuilding(planet, BuildingType.MetalMine)).MetalProduction + planet.Metal.BaseProduction) / 3600);
                planet.Crystal = new Crystal(planet.Crystal.Amount + (((CrystalMine)ObjectContainer.Instance.GetBuilding(planet, BuildingType.CrystalMine)).CrystalProduction + planet.Crystal.BaseProduction) / 3600);
                planet.Deuterium = new Deuterium(planet.Deuterium.Amount + (((DeuteriumExtractor)ObjectContainer.Instance.GetBuilding(planet, BuildingType.DeuteriumExtractor)).DeuteriumProduction + planet.Deuterium.BaseProduction) / 3600);
            }

            Synchronize(ObjectContainer.Instance.CurrentSelectedPlanet);
        }

    }
}
