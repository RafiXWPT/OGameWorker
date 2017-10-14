﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;
using Worker.Objects.Missions;
using Worker.Parser.Buildings;
using Worker.Parser.Galaxy;
using Worker.Parser.Movement;
using Worker.Parser.Planets;
using Worker.Parser.Resources;
using Worker.Parser.Ships;
using Worker.Parser.Technologies;

namespace Worker.HttpModule.Clients.DataProviders
{
    public class OGameDataProvider
    {
        public OGameDataProvider()
        {
            MissionsParser = new MissionsParser();
            FleetMovementParser = new FleetMovementParser();
            PlanetsParser = new PlanetsParser();
            BuildingsParser = new BuildingsParser();
            ResourcesParser = new ResourcesParser();
            TechnologiesParser = new TechnologiesParser();
            ShipsParser = new ShipsParser();
            GalaxyParser = new GalaxyParser();
        }

        public MissionsParser MissionsParser { get; }
        public FleetMovementParser FleetMovementParser { get; }
        public PlanetsParser PlanetsParser { get; }
        public BuildingsParser BuildingsParser { get; }
        public TechnologiesParser TechnologiesParser { get; }
        public ResourcesParser ResourcesParser { get; }
        public ShipsParser ShipsParser { get; }
        public GalaxyParser GalaxyParser { get; }

        public async Task<int> GetMissionReturnId(HtmlDocument document, MissionBase mission)
        {
            return await FleetMovementParser.GetReturnIdForMission(document, mission);
        }

        public async Task UpdateMissions(HtmlDocument document)
        {
            ObjectContainer.Instance.Missions = await MissionsParser.GetMissions(document);
        }

        public async Task UpdatePlayerPlanets(HtmlDocument document)
        {
            ObjectContainer.Instance.PlayerPlanets = await PlanetsParser.GetPlayerPlanets(document);
        }

        public async Task UpdatePlanetResources(HtmlDocument document, Planet planet)
        {
            await ResourcesParser.GetPlanetResources(planet, document);
        }

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

        public async Task UpdatePlanetTechnologies(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerTechnologies.RemoveAll(t => t.BelongsTo.Id == planet.Id);
            var planetTechnologies = await TechnologiesParser.GetTechnologies(document, planet);
            ObjectContainer.Instance.PlayerTechnologies.AddRange(planetTechnologies);
        }

        public async Task UpdatePlanetFleet(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerShips.RemoveAll(s => s.BelongsTo.Id == planet.Id);
            var planetShips = await ShipsParser.GetShips(document, planet);
            ObjectContainer.Instance.PlayerShips.AddRange(planetShips);
        }

        public async Task<List<GalaxyPlanetInfo>> ReadGalaxyPlanets(HtmlDocument document, int galaxy, int system)
        {
            return await GalaxyParser.GetPlanets(document, galaxy, system);
        }
    }
}