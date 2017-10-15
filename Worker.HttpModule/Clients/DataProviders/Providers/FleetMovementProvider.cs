using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Missions;
using Worker.Parser.Movement;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class FleetMovementProvider
    {
        public FleetMovementParser FleetMovementParser { get; } = new FleetMovementParser();

        public async Task<int> GetMissionReturnId(HtmlDocument document, MissionBase mission)
        {
            return await FleetMovementParser.GetReturnIdForMission(document, mission);
        }
    }
}
