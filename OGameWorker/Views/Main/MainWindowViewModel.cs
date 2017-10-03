using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using OGameWorker.Views.Main.Resources;
using ReactiveUI;
using Worker.HttpModule.Clients;
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
            Observable.Interval(TimeSpan.FromMinutes(5))
                .SubscribeOnDispatcher()
                .Subscribe(
                    async t =>
                    {
                       await RefreshObjectContainerTask();
                    });

            Init();
        }

        private void Init()
        {
            Task.Run(async () =>
            {
                await RefreshObjectContainerTask();
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
