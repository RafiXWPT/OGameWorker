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
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Buildings.Resource;
using Worker.Objects.Galaxy;
using Worker.Objects.Resources;

namespace OGameWorker.Views.Main.Resources
{
    public class ResourcesViewModel : OGameWorkerBaseViewModel
    {
        private Metal _metal;
        private Crystal _crystal;
        private Deuterium _deuterium;
        private Planet _planet;

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

        public Planet Planet
        {
            get => _planet;
            set => this.RaiseAndSetIfChanged(ref _planet, value);
        }

        public ResourcesViewModel() { }

        public ResourcesViewModel(OGameHttpClient client) : base(client)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(i => ObjectContainer.Instance.CurrentSelectedPlanet != null)
                .SubscribeOnDispatcher()
                .Subscribe(token =>
                {
                    UpdateResourcesLive();
                });
        }

        public void Synchronize(Planet currentPlanet)
        {
            Metal = currentPlanet.Metal;
            Crystal = currentPlanet.Crystal;
            Deuterium = currentPlanet.Deuterium;
        }

        private void UpdateResourcesLive()
        {
            var currentPlanet = ObjectContainer.Instance.CurrentSelectedPlanet;
            Metal = new Metal(Metal.Amount +
                              (((MetalMine) ObjectContainer.Instance.GetBuilding(currentPlanet, BuildingType.MetalMine))
                               .MetalProduction + Metal.BaseProduction) / 3600);
            Crystal = new Crystal(Crystal.Amount +
                                  (((CrystalMine) ObjectContainer.Instance.GetBuilding(currentPlanet,
                                       BuildingType.CrystalMine)).CrystalProduction + Crystal.BaseProduction) / 3600);
            Deuterium = new Deuterium(Deuterium.Amount +
                                      (((DeuteriumExtractor) ObjectContainer.Instance.GetBuilding(currentPlanet,
                                           BuildingType.DeuteriumExtractor)).DeuteriumProduction +
                                       Deuterium.BaseProduction) / 3600);
        }

    }
}
