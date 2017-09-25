using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;
using Worker.Objects.Ships;

namespace Worker.HttpModule.Clients.FleetSender
{
    public enum FleetSpeed
    {
        Speed10 = 1,
        Speed20 = 2,
        Speed30 = 3,
        Speed40 = 4,
        Speed50 = 5,
        Speed60 = 6,
        Speed70 = 7,
        Speed80 = 8,
        Speed90 = 9,
        Speed100 = 10
    }

    public class OGameFleetSender
    {
        private readonly OGameHttpClient _client;
        private readonly Planet _destination;
        private readonly FleetSpeed _speed;
        private readonly MissionType _missionType;
        private readonly List<ShipBase> _ships;

        public OGameFleetSender(OGameHttpClient client, Planet destination, FleetSpeed speed, MissionType missionType, List<ShipBase> ships)
        {
            _client = client;
            _destination = destination;
            _speed = speed;
            _missionType = missionType;
            _ships = ships;
        }

        public async Task<bool> SendFleet()
        {
            var result = await Task.Run(async () =>
            {
                await _client.SendHttpRequest(_client.Builder.BuildFleetSendingRequest1());
                await _client.SendHttpRequest(_client.Builder.BuildFleetSendingRequest2(_destination, _missionType, _speed, _ships));
                var sendFleetForm = await _client.SendHttpRequest(_client.Builder.BuildFleetSendingRequest3(_destination, _missionType, _speed, _ships));
                var sendFleetToken = sendFleetForm.ResponseHtmlDocument.DocumentNode.Descendants("input").First(i => i.Attributes.Any(a => a.OriginalName == "name" && a.Value == "token")).GetAttributeValue("value", null);
                return await _client.SendHttpRequest(_client.Builder.BuildFleetSendingRequest4(sendFleetToken, _destination, _missionType, _speed, _ships));
            });

            return result.StatusCode == HttpStatusCode.OK;
        }
    }
}
