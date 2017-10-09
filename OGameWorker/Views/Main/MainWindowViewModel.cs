using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using OGameWorker.Code;
using OGameWorker.Code.Extensions.Reactive;
using OGameWorker.Code.WorkerQueue;
using OGameWorker.Views.Main.Resources;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;
using Worker.Objects.Ships;
using Worker.Objects.Ships.Fighter;

namespace OGameWorker.Views.Main
{
    public class MainWindowViewModel : OGameWorkerBasicViewModel
    {
        public ResourcesViewModel ResourcesViewModel { get; }

        public MainWindowViewModel() { }

        public MainWindowViewModel(OGameHttpClient client) : base(client) 
        {
            ResourcesViewModel = new ResourcesViewModel(client);

            var random = new Random();
            Observable.Interval(TimeSpan.FromMinutes(5))
                .SubscribeOnDispatcher()
                .DelayTask(() => TimeSpan.FromMinutes(random.Next(10, 20)))
                .Select(i => SafeHttpTask(RefreshObjectContainerTask(false)))
                .Subscribe();

            Observable.Interval(TimeSpan.FromMinutes(1))
                .SubscribeOnDispatcher()
                .Where(i => WorkerQueue.QueueActions.Any())
                .Subscribe(token =>
                {
                    WorkerQueue.CheckQueue();
                });

            Init();
        }

        private void Init()
        {
            Task.Run(async () =>
            {
                await SafeHttpTask(RefreshObjectContainerTask(true));
            });
        }

        private async Task RefreshObjectContainerTask(bool force)
        {
            await Client.RefreshObjectContainer(force);
            ObjectContainer.Instance.CurrentSelectedPlanet = ObjectContainer.Instance.PlayerPlanets.First();
            ResourcesViewModel.Synchronize(ObjectContainer.Instance.CurrentSelectedPlanet);
            var hostileMissions = ObjectContainer.Instance.Missions.Where(m => m.MovementType == MovementType.Hostile).ToList();
            if (hostileMissions.Any())
            {
                foreach (var mission in hostileMissions)
                {
                    if (WorkerQueue.QueueActions.All(a => a.MissionId != mission.MissionId))
                    {
                        WorkerQueue.QueueAction(new QueueAction
                        {
                            MissionId = mission.MissionId,
                            Action = async () =>
                            {
                                var fleetSaveMission = await OGameFleetSender.SaveFleet(Client, mission.DestinationId ?? 0);
                                if (fleetSaveMission != null)
                                {
                                    WorkerQueue.QueueAction(new QueueAction
                                    {
                                        MissionId = fleetSaveMission.MissionId,
                                        Action = async () =>
                                        {
                                            await Client.ReturnMission(fleetSaveMission);
                                        },
                                        ExecutionTime = DateTime.Now.AddMinutes(10)
                                    });
                                }
                                else
                                {
                                    throw new Exception("Utworzenie misji fleetSave nie powiodło się");
                                }
                            },
                            ExecutionTime = mission.ArrivalTime.AddMinutes(-120)
                        });
                    }
                }
            }
        }
    }
}
