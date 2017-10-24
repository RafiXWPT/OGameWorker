using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Worker.HttpModule.Clients.OGameClient;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Missions;
using Worker.Objects.Resources;
using Worker.Objects.Structures.Ships;
using Worker.Objects.Structures.Ships.Other;

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
        private readonly Planet _source;
        private readonly Planet _destination;
        private readonly Metal _metal;
        private readonly Crystal _crystal;
        private readonly Deuterium _deuterium;
        private readonly MissionType _missionType;
        private readonly List<ShipBase> _ships;
        private readonly FleetSpeed _speed;

        private OGameFleetSender(OGameHttpClient client, Planet source, Planet destination, FleetSpeed speed, List<ShipBase> ships,
            MissionType missionType, Metal metal = null, Crystal crystal = null, Deuterium deuterium = null)
        {
            _client = client;
            _source = source;
            _destination = destination;
            _metal = metal;
            _crystal = crystal;
            _deuterium = deuterium;
            _speed = speed;
            _ships = ships;
            _missionType = missionType;
        }

        public static OGameFleetSender Attack(OGameHttpClient client, Planet source, Planet destination, FleetSpeed speed,
            List<ShipBase> ships)
        {
            return new OGameFleetSender(client, source, destination, speed, ships, MissionType.Attack);
        }

        public static OGameFleetSender Transport(OGameHttpClient client, Planet source, Planet destination, FleetSpeed speed,
            List<ShipBase> ships, Metal metal, Crystal crystal, Deuterium deuterium)
        {
            return new OGameFleetSender(client, source, destination, speed, ships, MissionType.Transport, metal,
                crystal,
                deuterium);
        }

        public static OGameFleetSender Stationize(OGameHttpClient client, Planet source, Planet destination, FleetSpeed speed,
            List<ShipBase> ships, Metal metal, Crystal crystal, Deuterium deuterium)
        {
            return new OGameFleetSender(client, source, destination, speed, ships, MissionType.Stationize, metal,
                crystal,
                deuterium);
        }

        public static OGameFleetSender Espionage(OGameHttpClient client, Planet source, Planet destination)
        {
            return new OGameFleetSender(client, source, destination, FleetSpeed.Speed100,
                new List<ShipBase> {new EspionageProbe(source, 1, true, true)}, MissionType.Espionage);
        }

        public static async Task<MissionBase> SaveFleet(OGameHttpClient client, int savePlanetId)
        {
            var planetToSave = ObjectContainer.Instance.PlayerPlanets.First(p => p.Id == savePlanetId);
            var saveLocation = ObjectContainer.Instance.PlayerPlanets.FirstOrDefault(p => p.Id != savePlanetId);
            if (saveLocation == null)
                return null;

            await client.RefreshPlanet(planetToSave);

            var planetShips = planetToSave.Ships;
            var shipsCapacity = planetShips.Sum(x => x.Capacity*x.Quantity)*0.95;

            Metal metalToSave;
            Crystal crystalToSave;
            Deuterium deuteriumToSave;

            planetToSave.Deuterium.Amount = (int)(planetToSave.Deuterium.Amount * 0.95);

            if (shipsCapacity > planetToSave.TotalResources)
            {
                metalToSave = planetToSave.Metal;
                crystalToSave = planetToSave.Crystal;
                deuteriumToSave = planetToSave.Deuterium;
            }
            else
            {
                metalToSave = new Metal((int)shipsCapacity * 1 / 6);
                crystalToSave = new Crystal((int)shipsCapacity * 2 / 6);
                deuteriumToSave = new Deuterium((int)shipsCapacity * 3 / 6);
                if (metalToSave.Amount > planetToSave.Metal.Amount)
                    metalToSave = planetToSave.Metal;
                if (crystalToSave.Amount > planetToSave.Crystal.Amount)
                    crystalToSave = planetToSave.Crystal;
                if (deuteriumToSave.Amount > planetToSave.Deuterium.Amount)
                    deuteriumToSave = planetToSave.Deuterium;
            }


            return await Stationize(client, planetToSave, saveLocation, FleetSpeed.Speed10, planetShips, metalToSave, crystalToSave, deuteriumToSave).SendFleet();
        }

        public async Task<MissionBase> SendFleet()
        {
            var result = await Task.Run(async () =>
            {
                await _client.SendHttpRequest(_client.Builder.BuildOverviewRequest(_source.Id));
                await _client.SendHttpRequest(_client.Builder.BuildFleetSendingRequest1());
                await _client.SendHttpRequest(
                    _client.Builder.BuildFleetSendingRequest2(_source, _ships, _speed, _missionType));
                var sendFleetForm =
                    await _client.SendHttpRequest(
                        _client.Builder.BuildFleetSendingRequest3(_destination, _ships, _speed, _missionType));
                var sendFleetToken = sendFleetForm.ResponseHtmlDocument.DocumentNode.Descendants("input")
                    .First(i => i.Attributes.Any(a => a.OriginalName == "name" && a.Value == "token"))
                    .GetAttributeValue("value", null);
                return await _client.SendHttpRequest(_client.Builder.BuildFleetSendingRequest4(sendFleetToken,
                    _destination, _ships, _speed, _missionType, _metal ?? new Metal(0), _crystal ?? new Crystal(0),
                    _deuterium ?? new Deuterium(0)));
            });

            if (result.StatusCode != HttpStatusCode.OK)
                return null;

            var missionsBefore = ObjectContainer.Instance.Missions.ToList();
            await _client.RefreshMissions(true);
            var missionsAfter = ObjectContainer.Instance.Missions.ToList();

            return missionsAfter.FirstOrDefault(mission => missionsBefore.All(m => m.MissionId != mission.MissionId));
        }
    }
}