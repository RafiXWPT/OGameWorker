using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Buildings.Resource;
using Worker.Objects.Structures.Buildings.Station;
using Worker.Objects.Structures.Buildings.Warehouse;

namespace Worker.Parser.Buildings
{
    public class BuildingsParser
    {
        private HtmlNode GetResourceBuildingNode(IEnumerable<HtmlNode> buildingsNode, BuildingType type)
        {
            return buildingsNode.FirstOrDefault(n => n.HasAttributes &&
                                                     n.Attributes.Any(a => a.Value.Contains($"supply{(int) type}")));
        }

        private HtmlNode GetStationBuildingNode(IEnumerable<HtmlNode> buildingsNode, BuildingType type)
        {
            return buildingsNode.FirstOrDefault(n => n.HasAttributes &&
                                                     n.Attributes.Any(a => a.Value.Contains($"station{(int) type}")));
        }

        private int GetBuildingLevel(HtmlNode buildingNode)
        {
            int level;

            var levelValueString = buildingNode
                .Descendants("span")
                .First(n => n.HasAttributes && n.Attributes.Any(a => a.Value.Contains("level")))
                .InnerHtml.Trim();

            if (int.TryParse(levelValueString, out level))
                return level;

            levelValueString = levelValueString.Split(new[] {"</span>"}, StringSplitOptions.RemoveEmptyEntries)[1]
                .Trim();

            if (int.TryParse(levelValueString, out level))
                return level;

            return levelValueString.Contains("span")
                ? Convert.ToInt32(levelValueString.Split(new[] {"<span"}, StringSplitOptions.RemoveEmptyEntries)[0]
                    .Trim())
                : 0;
        }

        private string CanUpgradeStatus(HtmlNode buildingNode)
        {
            return buildingNode?.ParentNode?.Attributes?.First(a => a.OriginalName == "class")?.Value;
        }

        private BuildingBase GetResourceBuilding(BuildingType type, IEnumerable<HtmlNode> buildingsNode, Planet planet)
        {
            var buildingNode = GetResourceBuildingNode(buildingsNode, type);
            var buildingLevel = GetBuildingLevel(buildingNode);
            var canUpgradeStatus = CanUpgradeStatus(buildingNode);
            var techReached = canUpgradeStatus != "off";
            var canUpgrade = techReached && canUpgradeStatus != "disabled";
            switch (type)
            {
                case BuildingType.MetalMine:
                    return new MetalMine(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.CrystalMine:
                    return new CrystalMine(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.DeuteriumExtractor:
                    return new DeuteriumExtractor(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.SolarPowerPlant:
                    return new SolarPowerPlant(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.FusionReactor:
                    return new FusionReactor(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.SolarSatellite:
                    return new SolarSatellite(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.MetalStorage:
                    return new MetalStorage(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.CrystalStorage:
                    return new CrystalStorage(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.DeuteriumTank:
                    return new DeuteriumTank(planet, buildingLevel, techReached, canUpgrade);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private BuildingBase GetStationBuilding(BuildingType type, IEnumerable<HtmlNode> buildingsNode, Planet planet)
        {
            var buildingNode = GetStationBuildingNode(buildingsNode, type);
            var buildingLevel = GetBuildingLevel(buildingNode);
            var canUpgradeStatus = CanUpgradeStatus(buildingNode);
            var techReached = canUpgradeStatus != "off";
            var canUpgrade = techReached && canUpgradeStatus != "disabled";
            switch (type)
            {
                case BuildingType.RoboticsFactory:
                    return new RoboticsFactory(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.Shipyard:
                    return new Shipyard(planet, buildingLevel, techReached, canUpgrade);
                case BuildingType.ResearchLabolatory:
                    return new ResearchLabolatory(planet, buildingLevel, techReached, canUpgrade);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<string> GetUpgradeBuildingUrl(HtmlDocument document, BuildingType type)
        {
            return await Task.Run(() =>
            {
                var upgradeBuildingHtml =
                    document
                        .DocumentNode
                        .Descendants("div")
                        .First(n => n
                            .Attributes
                            .Any(a => a.Value.Contains($"supply{(int) type}")))
                        .Descendants("a")
                        .First(n => n
                            .Attributes
                            .Any(a => a.OriginalName == "onclick"))
                        .GetAttributeValue("onclick", null);

                return Regex.Match(upgradeBuildingHtml, @"\'([^']*)\'").Groups[1].Value;
            });
        }

        public async Task<List<BuildingBase>> GetResourceBuildings(HtmlDocument document, Planet planet)
        {
            return await Task.Run(() =>
            {
                var planetBuildings = new List<BuildingBase>();
                var buildingsNode = document.GetElementbyId("building").Descendants("div");
                foreach (var buildingType in ResourceBuildings.List)
                    planetBuildings.Add(GetResourceBuilding(buildingType, buildingsNode, planet));

                var storageNode = document.GetElementbyId("storage").Descendants("div");
                foreach (var buildingType in StorageBuildings.List)
                    planetBuildings.Add(GetResourceBuilding(buildingType, storageNode, planet));

                return planetBuildings;
            });
        }

        public async Task<List<BuildingBase>> GetStationBuildings(HtmlDocument document, Planet planet)
        {
            return await Task.Run(() =>
            {
                var planetBuildings = new List<BuildingBase>();
                var buildingsNode = document.GetElementbyId("stationbuilding").Descendants("div");
                foreach (var buildingType in StationBuildings.List)
                    planetBuildings.Add(GetStationBuilding(buildingType, buildingsNode, planet));
                return planetBuildings;
            });
        }
    }
}