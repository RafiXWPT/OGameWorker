﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

                    var planetCoordsParts = planet.Descendants("span")
                        .First(n => n.Attributes.Any(a => a.Value.Contains("planet-koords")))
                        .InnerText.Trim().Replace("[", string.Empty).Replace("]", string.Empty).Split(':');
                    var planetPosition = new Planet.PlanetPosition
                    {
                        Galaxy = int.Parse(planetCoordsParts[0]),
                        System = int.Parse(planetCoordsParts[1]),
                        Planet = int.Parse(planetCoordsParts[2])
                    };

                    var planetInfoString = planet.Descendants("a")
                        .First()
                        .Attributes.First(a => a.OriginalName == "title")
                        .Value.Trim()
                        .Split(';')
                        .Where(v => v.EndsWith("&lt") && v != "&lt")
                        .First(v => v.Contains(" do "));
                    var temperatureParts = planetInfoString.Split(new[] { "do" }, StringSplitOptions.RemoveEmptyEntries);
                    var planetTemperature = new Planet.PlanetTemperature
                    {
                        Min = int.Parse(Regex.Match(temperatureParts[0], @"-?\d+").Value),
                        Max = int.Parse(Regex.Match(temperatureParts[1], @"-?\d+").Value)
                    };

                    planetList.Add(new Planet(planetId, planetName, planetTemperature, planetPosition));
                }

                return planetList;
            });       

            return playerPlanets;
        }
    }
}
