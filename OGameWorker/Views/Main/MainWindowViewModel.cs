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
using OGameWorker.Code.WorkerQueue;
using OGameWorker.Views.Main.Fleet;
using OGameWorker.Views.Main.Resources;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.Objects;
using Worker.Objects.Missions;

namespace OGameWorker.Views.Main
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly OGameHttpClient _client;
        private readonly WorkerQueue _queue;
        public ResourcesViewModel ResourcesViewModel { get; }

        public MainWindowViewModel() { }

        public MainWindowViewModel(OGameHttpClient client)
        {
            _client = client;
            _queue = new WorkerQueue();
            ResourcesViewModel = new ResourcesViewModel(client);

            var random = new Random();
            Observable.Interval(TimeSpan.FromMinutes(5))
                .SubscribeOnDispatcher()
                .DelayTask(() => TimeSpan.FromMinutes(random.Next(10, 20)))
                .Select(i => RefreshObjectContainerTask())
                .Subscribe();

            Observable.Interval(TimeSpan.FromMinutes(1))
                .SubscribeOnDispatcher()
                .Where(i => _queue.QueueActions.Any())
                .Subscribe(token =>
                {
                    _queue.ExecuteByTime();
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
            var hostileMissions = ObjectContainer.Instance.Missions.Where(m => m.MovementType == MovementType.Hostile).ToList();
            if (hostileMissions.Any())
            {
                foreach (var mission in hostileMissions)
                {
                    if (_queue.QueueActions.All(a => a.MissionId != mission.MissionId))
                    {
                        _queue.QueueAction(new QueueAction
                        {
                            Action = async () => { await OGameFleetSender.SaveFleet(_client, ObjectContainer.Instance.PlayerPlanets.First()); },
                            ExecutionTime = mission.ArrivalTime.AddMinutes(-15)
                        });
                    }
                }
            }

            ObjectContainer.Instance.CurrentSelectedPlanet = ObjectContainer.Instance.PlayerPlanets.First();
            ResourcesViewModel.Synchronize(ObjectContainer.Instance.CurrentSelectedPlanet);
        }
    }
}
