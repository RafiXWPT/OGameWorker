using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Worker.HttpModule.Clients.DataProviders;
using Worker.HttpModule.Exceptions;
using Worker.HttpModule.RequestBuilder;
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;

namespace Worker.HttpModule.Clients
{
    public class OGameHttpClient
    {
        private readonly string _username;
        private readonly string _password;
        private readonly Random _waiter = new Random();
        public readonly HttpClient _httpClient;
        private readonly SemaphoreSlim _lockObject = new SemaphoreSlim(1);

        public OGameHttpClient(string server, string username, string password)
        {
            _username = username;
            _password = password;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4,pt;q=0.2");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            Server = server;
            Builder = new OGameWorkerRequestBuilder(this);
            DataProvider = new OGameDataProvider();
        }

        public string Server { get; }
        public string ServerUrl => $"https://{Server}";
        public OGameWorkerRequestBuilder Builder { get; }
        public OGameDataProvider DataProvider { get; }

        public async Task<MessageContainer> ReLogIn()
        {
            return await SendHttpRequest(Builder.BuildLoginRequest(_username, _password), true);
        }

        public async Task<MessageContainer> LogIn(string username, string password)
        {
            return await SendHttpRequest(Builder.BuildLoginRequest(username, password), true);
        }

        public async Task RefreshMissions(bool force = false)
        {
            var missionsView = await SendHttpRequest(Builder.BuildEventListRequest(), force);
            await DataProvider.UpdateMissions(missionsView.ResponseHtmlDocument);
        }

        public async Task RefreshObjectContainer(bool force = false)
        {
            await RefreshMissions(force);
            var initialView = await SendHttpRequest(Builder.BuildOverviewRequest(), force);
            await DataProvider.UpdatePlayerPlanets(initialView.ResponseHtmlDocument);
            foreach (var planet in ObjectContainer.Instance.PlayerPlanets)
            {
                await RefreshPlanet(planet, force);
            }
        }

        public async Task RefreshPlanet(Planet planet, bool force = false)
        {
            var planetResources = await SendHttpRequest(Builder.BuildResourceRequest(planet.Id), force);
            await DataProvider.UpdatePlanetResources(planetResources.ResponseHtmlDocument, planet);
            await DataProvider.UpdatePlanetResourceBuildings(planetResources.ResponseHtmlDocument, planet);

            var planetStation = await SendHttpRequest(Builder.BuildStationRequest(planet.Id), force);
            await DataProvider.UpdatePlanetStationBuildings(planetStation.ResponseHtmlDocument, planet);

            var planetTechnologies = await SendHttpRequest(Builder.BuildResearchRequest(planet.Id), force);
            await DataProvider.UpdatePlanetTechnologies(planetTechnologies.ResponseHtmlDocument, planet);

            var planetShipyard = await SendHttpRequest(Builder.BuildShipyardRequest(planet.Id), force);
            await DataProvider.UpdatePlanetFleet(planetShipyard.ResponseHtmlDocument, planet);
        }

        public async Task<MessageContainer> UpgradeResourceBuilding(BuildingType type, Planet planet)
        {
            if (!ObjectContainer.Instance.GetBuilding(planet, type).CanBuild)
                return MessageContainer.Fail();

            var planetResources = await SendHttpRequest(Builder.BuildResourceRequest(planet.Id));
            var upgradeUrl =
                await DataProvider.BuildingsParser.GetUpgradeBuildingUrl(planetResources.ResponseHtmlDocument, type);
            var response = await SendHttpRequest(Builder.BuildUpgradeBuildingRequest(upgradeUrl));
            if (response.StatusCode == HttpStatusCode.OK)
                await RefreshObjectContainer();

            return response;
        }

        public async Task ReturnMission(MissionBase mission)
        {
            var movement = await SendHttpRequest(Builder.BuildMovementRequest());
            var missionReturnId = await DataProvider.GetMissionReturnId(movement.ResponseHtmlDocument, mission);
            await SendHttpRequest(Builder.BuildReturnMissionRequest(missionReturnId));
        }

        public async Task<List<GalaxyPlanetInfo>> GetGalaxyView(int galaxy, int system)
        {
            var galaxyView = await SendHttpRequest(Builder.BuildGalaxyViewRequest(galaxy, system));
            return await DataProvider.ReadGalaxyPlanets(galaxyView.ResponseHtmlDocument, galaxy, system);
        }

        public async Task<MessageContainer> SendHttpRequest(HttpRequestMessage request, bool force = false)
        {
            return await SendHttpRequestInternal(request, force);
        }

        private async Task<MessageContainer> SendHttpRequestInternal(HttpRequestMessage request, bool force = false)
        {
            MessageContainer messageContainer;
            await _lockObject.WaitAsync();
            try
            {
                if(!force)
                    await Task.Delay(TimeSpan.FromSeconds(_waiter.Next(0, 5)));
                var response = await _httpClient.SendAsync(request);
                messageContainer = new MessageContainer(request, response);
                if (await HasLoggedOut(messageContainer))
                {
                    throw new OgameWorkerLoggedOutException();
                }
            }
            finally
            {
                _lockObject.Release();
            }

            return messageContainer;
        }

        private static async Task<bool> HasLoggedOut(MessageContainer message) => await Task.Run(() => message.ResponseHtmlDocument.GetElementbyId("loginForm") != null);       
    }
}