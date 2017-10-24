using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;

namespace Worker.Parser.Movement
{
    public class MissionsParser
    {
        private int GetMissionId(HtmlNode missionNode)
        {
            return Convert.ToInt32(missionNode.Id.Replace("eventRow-", string.Empty));
        }

        private MissionType GetMissionType(HtmlNode missionNode)
        {
            return (MissionType) Convert.ToInt32(missionNode.Attributes
                .First(a => a.OriginalName == "data-mission-type")
                .Value);
        }

        private int GetArrivalTimestamp(HtmlNode missionNode)
        {
            return Convert.ToInt32(missionNode.Attributes.First(a => a.OriginalName == "data-arrival-time").Value);
        }

        private string GetSourcePlanetName(HtmlNode missionNode)
        {
            return missionNode.Descendants("td")
                .First(n => n.Attributes.Any(a => a.Value.Contains("originFleet")))
                .InnerText.Trim();
        }

        private string GetDestinationPlanetName(HtmlNode missionNode)
        {
            return missionNode.Descendants("td")
                .First(n => n.Attributes.Any(a => a.Value.Contains("destFleet")))
                .InnerText.Trim();
        }

        private Position GetSourcePlanetPosition(HtmlNode missionNode)
        {
            var positionParts = missionNode.Descendants("td")
                .First(n => n.Attributes.Any(a => a.Value.Contains("coordsOrigin")))
                .InnerText.Trim()
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Split(':');
            return new Position
            {
                Galaxy = Convert.ToInt32(positionParts[0]),
                System = Convert.ToInt32(positionParts[1]),
                Planet = Convert.ToInt32(positionParts[2])
            };
        }

        private Position GetDestinationPosition(HtmlNode missionNode)
        {
            var positionParts = missionNode.Descendants("td")
                .First(n => n.Attributes.Any(a => a.Value.Contains("destCoords")))
                .InnerText.Trim()
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Split(':');
            return new Position
            {
                Galaxy = Convert.ToInt32(positionParts[0]),
                System = Convert.ToInt32(positionParts[1]),
                Planet = Convert.ToInt32(positionParts[2])
            };
        }

        private bool GetIsReturning(HtmlNode missionNode)
        {
            return Convert.ToBoolean(missionNode.Attributes.First(a => a.OriginalName == "data-return-flight").Value);
        }

        public async Task<List<MissionBase>> GetFriendlyMissions(HtmlDocument document)
        {
            return await Task.Run(() =>
            {
                var friendlyMissions = new List<MissionBase>();
                var friendlyNodes = document.DocumentNode.Descendants("tr")
                    .Where(n => n.Id.Contains("eventRow") &&
                                n.Descendants("td").First().Attributes.Any(a => a.Value.Contains("friendly")));

                foreach (var friendlyNode in friendlyNodes)
                    if (GetIsReturning(friendlyNode))
                        friendlyMissions.Add(
                            new FriendlyMission(
                                GetMissionId(friendlyNode),
                                GetMissionType(friendlyNode),
                                GetArrivalTimestamp(friendlyNode),
                                new Planet(0, GetDestinationPlanetName(friendlyNode), GetDestinationPosition(friendlyNode)),
                                new Planet(0, GetSourcePlanetName(friendlyNode), GetSourcePlanetPosition(friendlyNode)),
                                true));
                    else
                        friendlyMissions.Add(
                            new FriendlyMission(
                                GetMissionId(friendlyNode),
                                GetMissionType(friendlyNode),
                                GetArrivalTimestamp(friendlyNode),
                                new Planet(0, GetSourcePlanetName(friendlyNode), GetSourcePlanetPosition(friendlyNode)),
                                new Planet(0, GetDestinationPlanetName(friendlyNode), GetDestinationPosition(friendlyNode)),
                                false));

                return friendlyMissions;
            });
        }

        public async Task<List<MissionBase>> GetNeutralMissions(HtmlDocument document)
        {
            return await Task.Run(() =>
            {
                var neutralMissions = new List<MissionBase>();
                var neutralNodes = document.DocumentNode.Descendants("tr")
                    .Where(n => n.Id.Contains("eventRow") &&
                                n.Descendants("td").First().Attributes.Any(a => a.Value.Contains("neutral")));

                foreach (var neutralNode in neutralNodes)
                    if (GetIsReturning(neutralNode))
                        neutralMissions.Add(
                            new NeutralMission(
                                GetMissionId(neutralNode),
                                GetMissionType(neutralNode),
                                GetArrivalTimestamp(neutralNode),
                                new Planet(0, GetDestinationPlanetName(neutralNode), GetDestinationPosition(neutralNode)),
                                new Planet(0, GetSourcePlanetName(neutralNode), GetSourcePlanetPosition(neutralNode)),
                                true));
                    else
                        neutralMissions.Add(
                            new NeutralMission(
                                GetMissionId(neutralNode),
                                GetMissionType(neutralNode),
                                GetArrivalTimestamp(neutralNode),
                                new Planet(0, GetSourcePlanetName(neutralNode), GetSourcePlanetPosition(neutralNode)),
                                new Planet(0, GetDestinationPlanetName(neutralNode), GetDestinationPosition(neutralNode)),
                                false));

                return neutralMissions;
            });
        }

        public async Task<List<MissionBase>> GetHostileMissions(HtmlDocument document)
        {
            return await Task.Run(() =>
            {
                var hostileMissions = new List<MissionBase>();
                var hostileNodes = document.DocumentNode.Descendants("tr")
                    .Where(n => n.Id.Contains("eventRow") &&
                                n.Descendants("td").First().Attributes.Any(a => a.Value.Contains("hostile")));

                foreach (var hostileNode in hostileNodes)
                    if (GetIsReturning(hostileNode))
                        hostileMissions.Add(
                            new HostileMission(
                                GetMissionId(hostileNode),
                                GetMissionType(hostileNode),
                                GetArrivalTimestamp(hostileNode),
                                new Planet(0, GetDestinationPlanetName(hostileNode), GetDestinationPosition(hostileNode)),
                                new Planet(0, GetSourcePlanetName(hostileNode), GetSourcePlanetPosition(hostileNode)),
                                true));
                    else
                        hostileMissions.Add(
                            new HostileMission(
                                GetMissionId(hostileNode),
                                GetMissionType(hostileNode),
                                GetArrivalTimestamp(hostileNode),
                                new Planet(0, GetSourcePlanetName(hostileNode), GetSourcePlanetPosition(hostileNode)),
                                new Planet(0, GetDestinationPlanetName(hostileNode), GetDestinationPosition(hostileNode)),
                                false));

                return hostileMissions;
            });
        }

        public async Task<List<MissionBase>> GetMissions(HtmlDocument document)
        {
            return await Task.Run(async () =>
            {
                var missionList = new List<MissionBase>();
                var friendlyMissions = await GetFriendlyMissions(document);
                var neutralMissions = await GetNeutralMissions(document);
                var hostileMissions = await GetHostileMissions(document);
                missionList.AddRange(friendlyMissions);
                missionList.AddRange(neutralMissions);
                missionList.AddRange(hostileMissions);
                return missionList;
            });
        }
    }
}