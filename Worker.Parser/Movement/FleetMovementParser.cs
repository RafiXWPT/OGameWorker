using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Missions;

namespace Worker.Parser.Movement
{
    public class FleetMovementParser
    {
        public async Task<int> GetReturnIdForMission(HtmlDocument document, MissionBase mission)
        {
            return await Task.Run(() =>
            {
                var missionReturnNode = document.GetElementbyId("inhalt")
                    .Descendants("div")
                    .First(n => n.Attributes.Any(a => a.OriginalName == "data-arrival-time" &&
                                                      a.Value == mission.ArrivalTimestamp.ToString()));

                var missionReturnId = missionReturnNode.Descendants("span")
                    .First(s => s.Attributes.Any(a => a.Value.Contains("reversal")))
                    .GetAttributeValue("ref", null);

                return Convert.ToInt32(missionReturnId);
            });
        }
    }
}
