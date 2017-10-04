using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using OGameWorker.Code.Extensions.Reactive;
using OGameWorker.Views.Main.Resources;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.Objects;

namespace OGameWorker.Views.Main
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly OGameHttpClient _client;
        public ResourcesViewModel ResourcesViewModel { get; }

        public MainWindowViewModel() { }

        public MainWindowViewModel(OGameHttpClient client)
        {
            _client = client;
            ResourcesViewModel = new ResourcesViewModel(client);
            var random = new Random();
            Observable.Interval(TimeSpan.FromMinutes(5))
                .SubscribeOnDispatcher()
                .DelayTask(() => TimeSpan.FromMinutes(random.Next(10, 20)))
                .Select(i => RefreshObjectContainerTask())
                .Subscribe();

            Init();
        }

        private void Init()
        {
            Task.Run(async () =>
            {
                await RefreshObjectContainerTask();
                await OGameFleetSender.SaveFleet(_client, ObjectContainer.Instance.PlayerPlanets.First());
            });
        }

        private async Task RefreshObjectContainerTask()
        {
            await _client.RefreshObjectContainer();
            ObjectContainer.Instance.CurrentSelectedPlanet = ObjectContainer.Instance.PlayerPlanets.First();
            ResourcesViewModel.Synchronize(ObjectContainer.Instance.CurrentSelectedPlanet);
        }
    }
}
