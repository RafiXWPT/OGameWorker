using System;
using System.Collections.Generic;
using System.Net.Http;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.HttpModule.Helpers;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;
using Worker.Objects.Ships;

namespace Worker.HttpModule.RequestBuilder
{
    public class OGameWorkerRequestBuilder
    {
        private readonly OGameHttpClient _client;

        public OGameWorkerRequestBuilder(OGameHttpClient client)
        {
            _client = client;
        }

        public HttpRequestMessage BuildOverviewRequest(int planetId = 0)
        {
            var url = planetId == 0
                ? $"{_client.ServerUrl}/game/index.php?page=overview"
                : $"{_client.ServerUrl}/game/index.php?page=overview&cp={planetId}";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildResourceRequest(int planetId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=resources&cp={planetId}";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildResearchRequest(int planetId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=research&cp={planetId}";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildStationRequest(int planetId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=station&cp={planetId}";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildShipyardRequest(int planetId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=shipyard&cp={planetId}";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildDefenseRequest(int planetId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=defense&cp={planetId}";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildMovementRequest()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=eventList&ajax=1";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildLoginRequest(string username, string password)
        {
            var url = "https://pl.ogame.gameforge.com:443/main/login";
            var content = new Dictionary<string, string>
            {
                {"kid", ""},
                {"login", username},
                {"pass", password},
                {"uni", _client.Server}
            };

            return new PostHttpRequest().Create(url, content);
        }

        public HttpRequestMessage BuildUpgradeBuildingRequest(string upgradeUrl)
        {
            return new GetHttpRequest().Create(upgradeUrl);
        }

        public HttpRequestMessage BuildPrepareFleetSendingRequest()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet1";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildFleetSendingRequest1()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet1";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildFleetSendingRequest2(Planet destination, MissionType type, FleetSpeed speed,
            List<ShipBase> ships)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet2";
            var values = new Dictionary<string, string>
            {
                {"galaxy", destination.Position.Galaxy.ToString()},
                {"system", destination.Position.System.ToString()},
                {"position", destination.Position.Planet.ToString()},
                {"type", ((int) type).ToString()},
                {"speed", ((int) speed).ToString()},
                {"mission", "0"}
            };

            foreach (var ship in ships)
                values.Add($"am{(int) ship.ShipType}", ship.Quantity.ToString());

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet1");
            return request;
        }

        public HttpRequestMessage BuildFleetSendingRequest3(Planet destination, MissionType type, FleetSpeed speed,
            List<ShipBase> ships)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet3";
            var values = new Dictionary<string, string>
            {
                {"galaxy", destination.Position.Galaxy.ToString()},
                {"system", destination.Position.System.ToString()},
                {"position", destination.Position.Planet.ToString()},
                {"type", ((int) type).ToString()},
                {"speed", ((int) speed).ToString()},
                {"mission", "0"},
                {"union", "0"}
            };

            foreach (var ship in ships)
                values.Add($"am{(int) ship.ShipType}", ship.Quantity.ToString());

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet2");
            return request;
        }

        public HttpRequestMessage BuildFleetSendingRequest4(string token, Planet destination, MissionType type,
            FleetSpeed speed, List<ShipBase> ships)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=movement";
            var values = new Dictionary<string, string>
            {
                {"token", token},
                {"galaxy", destination.Position.Galaxy.ToString()},
                {"system", destination.Position.System.ToString()},
                {"position", destination.Position.Planet.ToString()},
                {"type", ((int) type).ToString()},
                {"speed", ((int) speed).ToString()},
                {"holdingtime", "1"},
                {"mission", "1"},
                {"union2", "0"},
                {"holdingOrExpTime", "0"},
                {"prioMetal", "1"},
                {"prioCrystal", "2"},
                {"prioDeuterium", "3"},
                {"metal", "0"},
                {"crystal", "0"},
                {"deuterium", "0"},
                {"ajax", "1"}
            };

            foreach (var ship in ships)
                values.Add($"am{(int) ship.ShipType}", ship.Quantity.ToString());

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet3");
            return request;
        }
    }
}