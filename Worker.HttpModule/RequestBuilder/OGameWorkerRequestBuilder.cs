using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Helpers;
using Worker.Objects.Buildings;

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
            var url = planetId == 0 ? $"{_client.ServerUrl}/game/index.php?page=overview" : $"{_client.ServerUrl}/game/index.php?page=overview&cp={planetId}";
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

        public HttpRequestMessage BuildLoginRequest(string username, string password)
        {
            var url = "https://pl.ogame.gameforge.com:443/main/login";
            var content = new Dictionary<string,string>
            {
                {"kid", "" },
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
    }
}
