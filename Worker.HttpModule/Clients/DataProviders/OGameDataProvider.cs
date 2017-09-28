using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;
using Worker.Objects.Research;
using Worker.Parser.Buildings;
using Worker.Parser.Movement;
using Worker.Parser.Planets;
using Worker.Parser.Resources;
using Worker.Parser.Technologies;

namespace Worker.HttpModule.Clients.DataProviders
{
    public class OGameDataProvider
    {
        public MissionsParser MissionsParser { get; }
        public PlanetsParser PlanetsParser { get; }
        public BuildingsParser BuildingsParser { get; }
        public TechnologiesParser TechnologiesParser { get; }
        public ResourcesParser ResourcesParser { get; }

        public OGameDataProvider()
        {
            MissionsParser = new MissionsParser();
            PlanetsParser = new PlanetsParser();
            BuildingsParser = new BuildingsParser();
            ResourcesParser = new ResourcesParser();
            TechnologiesParser = new TechnologiesParser();
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
            ObjectContainer.Instance.PlayerBuildings.RemoveAll(b => b.BelongsTo.Id == planet.Id && ResourceBuildings.List.Contains(b.BuildingType));
            var planetResourceBuildings = await BuildingsParser.GetResourceBuildings(document, planet);
            ObjectContainer.Instance.PlayerBuildings.AddRange(planetResourceBuildings);
        }

        public async Task UpdatePlanetStationBuildings(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerBuildings.RemoveAll(b => b.BelongsTo.Id == planet.Id && StationBuildings.List.Contains(b.BuildingType));
            var planetStationBuildings = await BuildingsParser.GetStationBuildings(document, planet);
            ObjectContainer.Instance.PlayerBuildings.AddRange(planetStationBuildings);
        }

        public async Task UpdatePlanetTechnologies(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerTechnologies.RemoveAll(t => t.BelongsTo.Id == planet.Id);
            var planetBasicTechnologies = await TechnologiesParser.GetTechnologies(document, planet);
            ObjectContainer.Instance.PlayerTechnologies.AddRange(planetBasicTechnologies);
        }
    }
}
