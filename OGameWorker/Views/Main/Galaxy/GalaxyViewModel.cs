using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OGameWorker.Code;
using OGameWorker.Code.Extensions.Reactive;
using OGameWorker.Code.WorkerQueue;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.HttpModule.Clients.OGameClient;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Messages;

namespace OGameWorker.Views.Main.Galaxy
{
    public class GalaxyViewModel : OGameWorkerBaseViewModel
    {
        public bool IsBusy { get; set; }
        public bool IsSendingProbes { get; set; }
        public bool IsReadingEspionageReport { get; set; }

        private bool _isScanBotRunning;
        public bool IsScanBotRunning
        {
            get => _isScanBotRunning;
            set
            {
                _isScanBotRunning = value;
                IsEnableScanBotActive = !_isScanBotRunning;
                IsDisableScanBotActive = _isScanBotRunning;
            }
        }

        private bool _isEnableScanBotActive;
        public bool IsEnableScanBotActive
        {
            get => _isEnableScanBotActive;
            set => this.RaiseAndSetIfChanged(ref _isEnableScanBotActive, value);
        }

        private bool _isDisableScanBotActive;
        public bool IsDisableScanBotActive
        {
            get => _isDisableScanBotActive;
            set => this.RaiseAndSetIfChanged(ref _isDisableScanBotActive, value);
        }

        public ObservableCollection<GalaxyPlanetInfo> Planets
        {
            get => ObjectContainer.Instance.GalaxyPlanets;
            set => ObjectContainer.Instance.GalaxyPlanets = value;
        }

        public int Galaxy { get; set; }
        public int System { get; set; }
        public int Span { get; set; }

        public ReactiveCommand LoadGalaxyDataCommand { get; protected set; }
        public ReactiveCommand EnableScanBoxCommand { get; protected set; }
        public ReactiveCommand DisableScanBotCommand { get; protected set; }

        public GalaxyViewModel()
        {
            
        }

        public GalaxyViewModel(OGameHttpClient client) : base(client)
        {
            IsScanBotRunning = false;
            LoadGalaxyDataCommand = ReactiveCommand.CreateFromTask(t => LoadGalaxyDataTask(Galaxy, System, Span));
            EnableScanBoxCommand = ReactiveCommand.CreateFromTask(t => EnableScanBot());
            DisableScanBotCommand = ReactiveCommand.CreateFromTask(t => DisableScanBot());

            Observable.Interval(TimeSpan.FromSeconds(10))
                .Where(i => ObjectContainer.Instance.CurrentSelectedPlanet != null)
                .Where(i => IsScanBotRunning && Planets.Any())
                .Where(i => !IsSendingProbes)
                .SubscribeOnDispatcher()
                .DelayTask(() => TimeSpan.FromSeconds(Random.Next(0, 10)))
                .Select(i => ScanUnknownPlanet())
                .Subscribe();


            Observable.Interval(TimeSpan.FromSeconds(5))
                .Where(i => !IsReadingEspionageReport)
                .Where(i => ObjectContainer.Instance.Messages.Any(m => m.MessageType == MessageType.Espionage && m.MessageStatus == MessageStatus.New))
                .Select(i => ReadEspionageMessageTask())
                .Subscribe();
        }

        public async Task LoadGalaxyDataTask(int galaxy, int startSystem, int span)
        {
            await Task.Run(async () =>
            {
                IsBusy = true;

                var fromSystem = startSystem - span;
                var toSystem = startSystem + span;
                var inMemoryPlanets = new List<GalaxyPlanetInfo>();
                for (var i = fromSystem; i <= toSystem; i++)
                {
                    var galaxyPlanets = await Client.GetGalaxyView(galaxy, i);
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

        public Task EnableScanBot()
        {
            IsScanBotRunning = true;
            return Task.CompletedTask;
        }

        public Task DisableScanBot()
        {
            IsScanBotRunning = false;
            return Task.CompletedTask;
        }

        public async Task ScanUnknownPlanet()
        {
            IsSendingProbes = true;
            try
            {
                var notScanned = ObjectContainer.Instance.GalaxyPlanets.FirstOrDefault(
                    p => p.ScanStatus == ScanStatus.NotScanned && p.PlanetType == PlanetType.EnemyInactive);
                if (notScanned == null)
                {
                    await DisableScanBot();
                    return;
                }

                var mission = await OGameFleetSender.Espionage(Client, ObjectContainer.Instance.CurrentSelectedPlanet, Planet.FromGalaxyPlanetInfo(notScanned))
                    .SendFleet();
                if (mission == null)
                    return;

                WorkerQueue.QueueAction(new QueueAction
                {
                    MissionId = mission.MissionId,
                    ExecutionTime = mission.ArrivalTime.AddSeconds(5),
                    Action = async () =>
                    {
                        await Client.GetNewMessages(MessageType.Espionage);
                    }
                });

                notScanned.ScanStatus = ScanStatus.Scanning;
            }
            finally
            {
                IsSendingProbes = false;
            }
        }

        public async Task ReadEspionageMessageTask()
        {
            IsReadingEspionageReport = true;
            var espionageReport = ObjectContainer.Instance.Messages.FirstOrDefault(m => m.MessageType == MessageType.Espionage && m.MessageStatus == MessageStatus.New);
            if (espionageReport == null)
                return;

            await Client.ReadMessage(espionageReport);
            IsReadingEspionageReport = false;
        }

        public async Task SendEspionageProbesTask(GalaxyPlanetInfo info)
        {
            await OGameFleetSender.Espionage(Client, ObjectContainer.Instance.CurrentSelectedPlanet, Planet.FromGalaxyPlanetInfo(info)).SendFleet();
        }
    }
}
