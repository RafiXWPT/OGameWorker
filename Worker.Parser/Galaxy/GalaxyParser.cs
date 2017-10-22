using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;

namespace Worker.Parser.Galaxy
{
    public class GalaxyParser
    {
        private PlanetType ParsePlanetType(HtmlNode planetNode)
        {
            var planetAttributes = planetNode.Attributes.Select(a => a.OriginalName).ToArray();

            if (planetAttributes.Any(a => a.Contains("empty")))
            {
                return PlanetType.Empty;
            }

            if (planetAttributes.Any(a => a.Contains("newbie")))
            {
                return PlanetType.EnemyNewbie;
            }

            if (planetAttributes.Any(a => a.Contains("vacation")))
            {
                return PlanetType.EnemyVacation;
            }

            if (planetAttributes.Any(a => a.Contains("inactive")))
            {
                return PlanetType.EnemyInactive;
            }

            if (planetAttributes.Any(a => a.Contains("strong")))
            {
                return PlanetType.EnemyStrong;
            }

            return PlanetType.Self;
        }

        private GalaxyPlanetInfo ParsePlanet(HtmlNode planetNode, int galaxy, int system, int planet)
        {
            var planetType = ParsePlanetType(planetNode);
            var planetId = planetType == PlanetType.Empty
                ? 0
                : int.Parse(Regex.Match(planetNode.Descendants("td").ToList()[1].GetAttributeValue("data-planet-id", null), @"-?\d+").Value);
            var planetName = planetType == PlanetType.Empty
                ? ""
                : planetNode.Descendants("td").ToList()[2]
                    .InnerHtml.Replace("\\n", string.Empty)
                    .Trim()
                    .Split('<')
                    .First()
                    .Trim();
            var playerName = planetType == PlanetType.Empty
                ? ""
                : planetNode.Descendants("td").ToList()[5]
                    .Descendants("span").First()
                    .InnerHtml.Replace("\\n", string.Empty)
                    .Trim()
                    .Split('<')
                    .First();

            return new GalaxyPlanetInfo()
            {
                PlanetId = planetId,
                PlanetType = planetType,
                PlanetName = planetName,
                PlayerName = playerName,
                PlanetPosition = new Planet.PlanetPosition
                {
                    Galaxy = galaxy,
                    System = system,
                    Planet = planet
                }
            };
        }

        public async Task<List<GalaxyPlanetInfo>> GetPlanets(HtmlDocument document, int galaxy, int system)
        {
            return await Task.Run(() =>
            {
                var planets = new List<GalaxyPlanetInfo>();
                var galaxyTable = document.DocumentNode.Descendants("table").FirstOrDefault();
                if (galaxyTable == null)
                    return planets;

                var galaxyRows = galaxyTable.Descendants("tbody").First().Descendants("tr").ToArray();
                for (var i = 0; i < 15; i++)
                {
                    planets.Add(ParsePlanet(galaxyRows[i], galaxy, system, i+1));
                }

                return planets;
            });
        }
    }
}
