using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private Planet ParsePlanet(HtmlNode planetNode, int galaxy, int system, int planet)
        {
            var planetType = ParsePlanetType(planetNode);
            var planetName = planetType == PlanetType.Empty
                ? ""
                : planetNode.Descendants("td")
                    .First(d => d.HasAttributes && d.Attributes.Any(a => a.Value.Contains("planetname")))
                    .InnerHtml.Replace("\\n", string.Empty)
                    .Trim()
                    .Split('<')
                    .First();

            return new Planet(planetName, new Planet.PlanetPosition {Galaxy = galaxy, System = system, Planet = planet}) {PlanetType = planetType};
        }

        public async Task<List<Planet>> GetPlanets(HtmlDocument document, int galaxy, int system)
        {
            return await Task.Run(() =>
            {
                var planets = new List<Planet>();
                var galaxyRows = document.DocumentNode.Descendants("table").First().Descendants("tbody").First().Descendants("tr").ToArray();
                for (var i = 0; i < 15; i++)
                {
                    planets.Add(ParsePlanet(galaxyRows[i], galaxy, system, i+1));
                }

                return planets;
            });
        }
    }
}
