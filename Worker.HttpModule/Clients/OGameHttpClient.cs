﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Worker.HttpModule.Clients.DataProviders;
using Worker.HttpModule.RequestBuilder;
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;

namespace Worker.HttpModule.Clients
{
    public class OGameHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly SemaphoreSlim _lockObject = new SemaphoreSlim(1);

        public OGameHttpClient(string server)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4,pt;q=0.2");
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
            Server = server;
            Builder = new OGameWorkerRequestBuilder(this);
            DataProvider = new OGameDataProvider();
        }

        public string Server { get; }
        public string ServerUrl => $"https://{Server}";
        public OGameWorkerRequestBuilder Builder { get; }
        public OGameDataProvider DataProvider { get; }

        public async Task<MessageContainer> LogIn(string username, string password)
        {
            return await SendHttpRequest(Builder.BuildLoginRequest(username, password));
        }

        public async Task RefreshMissions()
        {
            var missionsView = await SendHttpRequest(Builder.BuildMovementRequest());
            await DataProvider.UpdateMissions(missionsView.ResponseHtmlDocument);
        }

        public async Task RefreshObjectContainer()
        {
            var initialView = await SendHttpRequest(Builder.BuildOverviewRequest());
            await DataProvider.UpdatePlayerPlanets(initialView.ResponseHtmlDocument);
            foreach (var planet in ObjectContainer.Instance.PlayerPlanets)
            {
                var planetResources = await SendHttpRequest(Builder.BuildResourceRequest(planet.Id));
                await DataProvider.UpdatePlanetResources(planetResources.ResponseHtmlDocument, planet);
                await DataProvider.UpdatePlanetResourceBuildings(planetResources.ResponseHtmlDocument, planet);

                var planetStation = await SendHttpRequest(Builder.BuildStationRequest(planet.Id));
                await DataProvider.UpdatePlanetStationBuildings(planetStation.ResponseHtmlDocument, planet);

                var planetTechnologies = await SendHttpRequest(Builder.BuildResearchRequest(planet.Id));
                await DataProvider.UpdatePlanetTechnologies(planetTechnologies.ResponseHtmlDocument, planet);

                var planetShipyard = await SendHttpRequest(Builder.BuildShipyardRequest(planet.Id));
                await DataProvider.UpdatePlanetFleet(planetShipyard.ResponseHtmlDocument, planet);
            }
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

        public async Task<MessageContainer> SendHttpRequest(HttpRequestMessage request)
        {
            return await SendHttpRequestInternal(request);
        }

        private async Task<MessageContainer> SendHttpRequestInternal(HttpRequestMessage request)
        {
            MessageContainer messageContainer;
            await _lockObject.WaitAsync();
            try
            {
                var response = await _httpClient.SendAsync(request);
                messageContainer = new MessageContainer(request, response);
            }
            finally
            {
                _lockObject.Release();
            }

            return messageContainer;
        }
    }
}