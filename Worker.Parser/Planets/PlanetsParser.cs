using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;

namespace Worker.Parser.Planets
{
    public class PlanetsParser
    {
        public async Task<List<Planet>> GetPlayerPlanets(HtmlDocument document)
        {
            var playerHtmlPlanets = await Task.Run(() => document.GetElementbyId("planetList").Descendants("div").Where(c => c.Attributes.Any(a => a.Value.Contains("smallplanet"))).ToList());
            var playerPlanets = await Task.Run(() =>
            {
                var planetList = new List<Planet>();

                foreach (var planet in playerHtmlPlanets)
                {
                    var planetId = Convert.ToInt32(planet.Id.Replace("planet-", string.Empty));
                    var planetName = planet.Descendants("span")
                        .First(n => n.Attributes.Any(a => a.Value.Contains("planet-name")))
                        .InnerText.Trim();
                    //var planetCoords = playerHtmlPlanets.First().Descendants("span").Where(n => n.Attributes.Any(a => a.Value.Contains("planet-koords"))).ToList()[0].InnerText.Trim()
                    planetList.Add(new Planet(planetId, planetName));
                }

                return planetList;
            });       

            return playerPlanets;
        }
    }
}
