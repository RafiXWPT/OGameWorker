using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Buildings;
using Worker.Objects.Buildings.ResourceBuildings;
using Worker.Objects.Galaxy;

namespace Worker.Parser.Buildings
{
    public class BuildingsParser
    {
        private IEnumerable<HtmlNode> GetBuildingNode(IEnumerable<HtmlNode> buildingNodes, BuildingType type)
        {
            return buildingNodes.FirstOrDefault(n => n.Attributes.Any(a => a.Value.Contains($"supply{(int) type}")))?.Descendants("span");
        }

        private int GetBuildingLevel(IEnumerable<HtmlNode> buildingNode, BuildingType type)
        {
            var levelValue = buildingNode
                ?.First(n => n.Attributes.Any(a => a.Value.Contains("level")))
                ?.InnerHtml
                ?.Split(new[] {"</span>"}, StringSplitOptions.RemoveEmptyEntries).Last()
                ?.Trim();
            return Convert.ToInt32(levelValue);
        }

        private bool IsBuildingTechReached(IEnumerable<HtmlNode> buildingNode)
        {
            return false;
        }

        private MetalMine GetMetalMine(IEnumerable<HtmlNode> buildingNodes, Planet planet)
        {
            var metalMineNode = GetBuildingNode(buildingNodes, BuildingType.MetalMine);
            var metalMineLevel = GetBuildingLevel(metalMineNode, BuildingType.MetalMine);
            var techReached = IsBuildingTechReached(metalMineNode);
            return new MetalMine(planet, metalMineLevel, techReached);
        }

        private CrystalMine GetCrystalMine(IEnumerable<HtmlNode> buildingNodes, Planet planet)
        {
            var crystalMineNode = GetBuildingNode(buildingNodes, BuildingType.CrystalMine);
            var crystalMineLevel = GetBuildingLevel(crystalMineNode, BuildingType.CrystalMine);
            var techReached = IsBuildingTechReached(crystalMineNode);
            return new CrystalMine(planet, crystalMineLevel, techReached);
        }

        private DeuteriumExtractor GetDeuteriumExtractor(IEnumerable<HtmlNode> buildingNodes, Planet planet)
        {
            var deuteriumExtractorNode = GetBuildingNode(buildingNodes, BuildingType.DeuteriumExtractor);
            var deuteriumExtractorLevel = GetBuildingLevel(deuteriumExtractorNode, BuildingType.DeuteriumExtractor);
            var techReached = IsBuildingTechReached(deuteriumExtractorNode);
            return new DeuteriumExtractor(planet, deuteriumExtractorLevel, techReached);
        }

        public async Task<List<BuildingBase>> GetResourceBuildings(HtmlDocument document, Planet planet)
        {
            var planetResourceBuildings = await Task.Run(() =>
            {
                var planetBuildings = new List<BuildingBase>();
                var buildingsNode = document.GetElementbyId("building").Descendants("div");
                planetBuildings.Add(GetMetalMine(buildingsNode, planet));
                planetBuildings.Add(GetCrystalMine(buildingsNode, planet));
                planetBuildings.Add(GetDeuteriumExtractor(buildingsNode, planet));
                return planetBuildings;
            });

            return planetResourceBuildings;
        }

        public async Task<List<BuildingBase>> GetStationBuildings(HtmlDocument document, Planet planet)
        {
            var planetResourceBuildings = await Task.Run(() =>
            {
                return "";
            });

            return new List<BuildingBase>();
        }
    }
}
