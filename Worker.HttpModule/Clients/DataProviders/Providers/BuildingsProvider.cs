﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;
using Worker.Parser.Buildings;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class BuildingsProvider
    {
        public BuildingsParser BuildingsParser { get; } = new BuildingsParser();

        public async Task UpdatePlanetResourceBuildings(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerBuildings.RemoveAll(
                b => b.BelongsTo.Id == planet.Id && ResourceBuildings.List.Contains(b.BuildingType));
            var planetResourceBuildings = await BuildingsParser.GetResourceBuildings(document, planet);
            ObjectContainer.Instance.PlayerBuildings.AddRange(planetResourceBuildings);
        }

        public async Task UpdatePlanetStationBuildings(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerBuildings.RemoveAll(
                b => b.BelongsTo.Id == planet.Id && StationBuildings.List.Contains(b.BuildingType));
            var planetStationBuildings = await BuildingsParser.GetStationBuildings(document, planet);
            ObjectContainer.Instance.PlayerBuildings.AddRange(planetStationBuildings);
        }
    }
}