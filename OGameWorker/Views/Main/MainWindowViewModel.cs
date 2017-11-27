using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using OGameWorker.Code;
using OGameWorker.Code.Extensions.Reactive;
using OGameWorker.Code.WorkerQueue;
using OGameWorker.Views.Main.Galaxy;
using OGameWorker.Views.Main.Galaxy.Tab;
using OGameWorker.Views.Main.Resources;
using OGameWorker.Views.Main.TopBar;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.HttpModule.Clients.OGameClient;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;

namespace OGameWorker.Views.Main
{
    public class MainWindowViewModel : OGameWorkerBaseViewModel
    {
        public ResourcesViewModel ResourcesViewModel { get; }
        public TopBarViewModel TopBarViewModel { get; }
        public GalaxyViewModel GalaxyViewModel { get; }

        public MainWindowViewModel() { }

        public MainWindowViewModel(OGameHttpClient client) : base(client) 
        {
            ResourcesViewModel = new ResourcesViewModel(client);
            TopBarViewModel = new TopBarViewModel(ResourcesViewModel);
            GalaxyViewModel = new GalaxyViewModel(client);

            Observable.Interval(TimeSpan.FromMinutes(5))
                .SubscribeOnDispatcher()
                .DelayTask(() => TimeSpan.FromMinutes(Random.Next(10, 20)))
                .Select(i => SafeHttpTask(() => RefreshObjectContainerTask(false)))
                .Subscribe();

            Observable.Interval(TimeSpan.FromMinutes(1))
                .SubscribeOnDispatcher()
                .Where(i => WorkerQueueActionRunner.ExecutionQueue.Any())
                .Subscribe(token =>
                {
                    WorkerQueueActionRunner.CheckQueue();
                });

            Init();
        }

        private void Init()
        {
            Task.Run(async () =>
            {
                await Client.ClearMessages(true);
                await SafeHttpTask(() => RefreshObjectContainerTask(true));
                ObjectContainer.Instance.Initialized = true;
                TopBarViewModel.ReadOnly = false;
            });
        }

        private async Task RefreshObjectContainerTask(bool force)
        {
            await Client.RefreshObjectContainer(force);
            ResourcesViewModel.Synchronize(ObjectContainer.Instance.CurrentSelectedPlanet);
            TopBarViewModel.UpdatePlayerPlanets(ObjectContainer.Instance.PlayerPlanets);
            CheckHostileMissions(); 
        }

        private void CheckHostileMissions()
        {
            var hostileMissions = ObjectContainer.Instance.Missions.Where(m => m.MovementType == MovementType.Hostile).ToList();
            if (!hostileMissions.Any())
                return;
            
            foreach (var mission in hostileMissions)
            {
                if (WorkerQueueActionRunner.ExecutionQueue.All(a => a.ActionId != mission.MissionId))
                {
                    WorkerQueueActionRunner.QueueAction(new TimeExecutionAction(mission.MissionId, ActionTarget.FleetSave)
                    {
                        Action = async () =>
                        {
                            var fleetSaveMission = await OGameFleetSender.SaveFleet(Client, mission.DestinationId ?? 0);
                            if (fleetSaveMission != null)
                            {
                                WorkerQueueActionRunner.QueueAction(new TimeExecutionAction(ActionTarget.ReturnFleet)
                                {
                                    Action = async () =>
                                    {
                                        await SafeHttpTask(() => Client.ReturnMission(fleetSaveMission));
                                    },
                                    ExecutionTime = DateTime.Now.AddMinutes(10)
                                });
                            }
                            else
                            {
                                throw new Exception("Utworzenie misji fleetSave nie powiodło się");
                            }
                        },
                        ExecutionTime = mission.ArrivalTime.AddMinutes(-5)
                    });
                }
            }
        }
    }
}
