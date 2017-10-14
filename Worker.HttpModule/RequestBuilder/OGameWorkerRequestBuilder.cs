using System;
using System.Collections.Generic;
using System.Net.Http;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.HttpModule.Helpers;
using Worker.Objects.Galaxy;
using Worker.Objects.Messages;
using Worker.Objects.Missions;
using Worker.Objects.Resources;
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

        public HttpRequestMessage BuildEventListRequest()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=eventList&ajax=1";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildMessagesRequest()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=messages";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildFleetMessagesRequest(MessageType type, int page = 1)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=messages&tab=2{(int)type}&pagination={page}&ajax=1";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildClearFleetMessagesRequest(MessageType type)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=messages";
            var values = new Dictionary<string,string>
            {
                { "tabid", $"2{(int)type}"},
                {"messageId", "-1" },
                {"action", "103" },
                {"ajax", "1" }
            };

            return new PostHttpRequest().Create(url, values);
        }

        public HttpRequestMessage BuildMessageDetailsRequest(int messageId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=messages&messageId={messageId}&ajax=1";
            return new GetHttpRequest().Create(url);
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
            var url = $"{_client.ServerUrl}/game/index.php?page=movement";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildReturnMissionRequest(int returnId)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=movement&return={returnId}";
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

        public HttpRequestMessage BuildGalaxyViewRequest(int galaxy, int system)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=galaxyContent&ajax=1";
            var values = new Dictionary<string,string>
            {
                {"galaxy", $"{galaxy}"},
                {"system", $"{system}" }
            };

            return new PostHttpRequest().Create(url, values);
        }

        public HttpRequestMessage BuildFleetSendingRequest1()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet1";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildFleetSendingRequest2(Planet source, List<ShipBase> ships, FleetSpeed speed, MissionType missionType)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet2";
            var values = new Dictionary<string, string>
            {
                {"galaxy", $"{source.Position.Galaxy}"},
                {"system", $"{source.Position.System}"},
                {"position", $"{source.Position.Planet}"},
                {"type", $"{(int) missionType}"},
                {"speed", $"{(int) speed}"},
                {"mission", "0"}
            };

            foreach (var ship in ships)
                values.Add($"am{(int) ship.ShipType}", $"{ship.Quantity}");

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet1");
            request.Headers.Add("Connection", "keep-alive");

            return request;
        }

        public HttpRequestMessage BuildFleetSendingRequest3(Planet destination, List<ShipBase> ships, FleetSpeed speed, MissionType missionType)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet3";
            var values = new Dictionary<string, string>
            {
                {"galaxy", $"{destination.Position.Galaxy}"},
                {"system", $"{destination.Position.System}"},
                {"position", $"{destination.Position.Planet}"},
                {"type", $"{(int) missionType}"},
                {"speed", $"{(int) speed}"},
                {"mission", "0"},
                {"union", "0"}
            };

            foreach (var ship in ships)
                values.Add($"am{(int)ship.ShipType}", $"{ship.Quantity}");

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet2");
            request.Headers.Add("Connection", "keep-alive");
            return request;
        }

        public HttpRequestMessage BuildFleetSendingRequest4(string token, Planet destination, List<ShipBase> ships, FleetSpeed speed, MissionType missionType, Metal metal, Crystal crystal, Deuterium deuterium)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=movement";
            var values = new Dictionary<string, string>
            {
                {"token", token},
                {"galaxy", $"{destination.Position.Galaxy}"},
                {"system", $"{destination.Position.System}"},
                {"position", $"{destination.Position.Planet}"},
                {"type", "1"},
                {"speed", $"{(int)speed}"},
                {"holdingtime", "1"},
                {"mission", $"{(int)missionType}"},
                {"union2", "0"},
                {"holdingOrExpTime", "0"},
                {"prioMetal", "1"},
                {"prioCrystal", "2"},
                {"prioDeuterium", "3"},
                {"metal", $"{metal.Amount}"},
                {"crystal", $"{crystal.Amount}"},
                {"deuterium", $"{deuterium.Amount}"},
                {"ajax", "1"}
            };

            foreach (var ship in ships)
                values.Add($"am{(int)ship.ShipType}", $"{ship.Quantity}");

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet3");
            request.Headers.Add("Connection", "keep-alive");
            return request;
        }  
    }
}