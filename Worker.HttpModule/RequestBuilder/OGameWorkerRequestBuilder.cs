using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.FleetSender;
using Worker.HttpModule.Helpers;
using Worker.Objects.Galaxy;
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

        public HttpRequestMessage BuildEventListRequest()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=eventList&ajax=1";
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

        public HttpRequestMessage BuildTransportRequest1()
        {
            var url =
                $"{_client.ServerUrl}/game/index.php?page=fleet1&galaxy=4&system=95&position=6&type=1&mission={(int) MissionType.Transport}";
            var request = new GetHttpRequest().Create(url);
            request.Headers.Add("Connection", "keep-alive");
            return request;
        }

        public HttpRequestMessage BuildTransportRequest2()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet2";
            var values = new Dictionary<string, string>
            {
                {"galaxy", "4"},
                {"system", "95" },
                {"position", "6" },
                {"type", "1"},
                {"mission", "3" },
                {"speed", "10" },
                {"am204", "8" },
                {"am202", "1" },
                {"am203", "25" }
            };

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet1");
            request.Headers.Add("Connection", "keep-alive");
            return request;
        }

        public HttpRequestMessage BuildTransportRequest3()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet3";
            var values = new Dictionary<string, string>
            {
                {"type", "1"},
                {"mission", "3" },
                {"union", "0" },
                {"am202", "1" },
                {"am203", "25" },
                {"am204", "8" },
                {"galaxy", "4"},
                {"system", "95" },
                {"position", "6" },
                {"acsValues", "-" },
                {"speed", "1" }
            };

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet2");
            request.Headers.Add("Connection", "keep-alive");
            return request;
        }

        public HttpRequestMessage BuildTransportRequest4(string token)
        {
            var url =
                $"{_client.ServerUrl}/game/index.php?page=movement&holdingtime=1&expeditiontime=1&token={token}&galaxy=4&system=95&position=6&type=1&mission=3&union2=0&holdingOrExpTime=0&speed=1&acsValues=-&prioMetal=1&prioCrystal=2&prioDeuterium=3&am202=1&am203=25&am204=8&metal=1000&crystal=1000&deuterium=1000&ajax=1";
            var values = new Dictionary<string, string>
            {
                {"token", token }
            };

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet3");
            request.Headers.Add("Connection", "keep-alive");
            return request;
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

        public HttpRequestMessage BuildTestStationary2()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet2";
            var values = new Dictionary<string,string>
            {
                {"galaxy", "4"},
                {"system", "91" },
                {"position", "10" },
                {"type", "1" },
                {"mission", "0" },
                {"speed", "1" },
                {"am204", "8" },
                {"am202", "1" },
                {"am203", "25" }
            };

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet1");
            return request;
        }

        public HttpRequestMessage BuildTestStationary3()
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet3";
            var values = new Dictionary<string, string>
            {
                {"type", "1" },
                {"mission", "0" },
                {"union", "0" },
                {"am202", "1" },
                {"am203", "25" },
                {"am204", "8" },
                {"galaxy", "4"},
                {"system", "95" },
                {"position", "6" },
                {"acsValues", "-" },
                {"speed", "1" }
            };

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet2");
            return request;
        }

        public HttpRequestMessage BuildFleetSendingRequest2(Planet source, MissionType type, FleetSpeed speed, List<ShipBase> ships)
        {
            var url = $"{_client.ServerUrl}/game/index.php?page=fleet2";
            var values = new Dictionary<string, string>
            {
                {"galaxy", source.Position.Galaxy.ToString()},
                {"system", source.Position.System.ToString()},
                {"position", source.Position.Planet.ToString()},
                {"type", "1"},
                {"mission", $"{(int)type}"},
                {"speed", $"{(int)speed}"}
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
                {"type", $"{(int)type}"},
                {"mission", "0"},
                {"union", "0"},
                {"galaxy", destination.Position.Galaxy.ToString()},
                {"system", destination.Position.System.ToString()},
                {"position", destination.Position.Planet.ToString()},
                {"speed", ((int) speed).ToString()}
            };

            foreach (var ship in ships)
                values.Add($"am{(int) ship.ShipType}", ship.Quantity.ToString());

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet2");
            return request;
        }

        public HttpRequestMessage BuildFleetSendingRequest4(string token, Planet destination, MissionType type, FleetSpeed speed, List<ShipBase> ships, Metal metal, Crystal crystal, Deuterium deuterium)
        {
            var url =
                $"{_client.ServerUrl}/game/index.php?page=movement&holdingtime=1&expeditiontime=1&token={token}&galaxy={destination.Position.Galaxy}&system={destination.Position.System}&position={destination.Position.Planet}&type=1&mission={(int) type}&union2=0&holdingOrExpTime=0&speed={(int) speed}&acsValues=-&prioMetal=1&prioCrystal=2&prioDeuterium=3&metal={metal.Amount}&crystal={crystal.Amount}&deuterium={deuterium.Amount}&ajax=1";

            foreach (var ship in ships)
            {
                url += $"&am{(int)ship.ShipType}={ship.Quantity}";
            }

            var values = new Dictionary<string, string>
            {
                {"token", token}
            };

            var request = new PostHttpRequest().Create(url, values);
            request.Headers.Referrer = new Uri($"{_client.ServerUrl}/game/index.php?page=fleet3");
            return request;
        }
    }
}