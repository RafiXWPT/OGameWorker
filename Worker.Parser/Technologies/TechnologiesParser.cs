using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Objects.Research;
using Worker.Objects.Research.Advanced;
using Worker.Objects.Research.Basic;
using Worker.Objects.Research.Combat;
using Worker.Objects.Research.Drive;

namespace Worker.Parser.Technologies
{
    public class TechnologiesParser
    {
        private HtmlNode GetTechnologyNode(IEnumerable<HtmlNode> technologiesNode, TechnologyType type)
        {
            return technologiesNode.FirstOrDefault(n => n.HasAttributes && n.Attributes.Any(a => a.Value.Contains($"research{(int) type}")));
        }

        private int GetTechnologyLevel(HtmlNode technologyNode)
        {
            var levelValueString = technologyNode
                .Descendants("span")
                .First(n => n.HasAttributes && n.Attributes.Any(a => a.Value.Contains("level")))
                .InnerHtml.Trim();

            if (int.TryParse(levelValueString, out int level))
            {
                return level;
            }

            levelValueString = levelValueString.Split(new[] { "</span>" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

            return levelValueString.Contains("span") ? Convert.ToInt32(levelValueString.Split(new[] { "<span" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim()) : 0;
        }

        private string CanUpgradeStatus(HtmlNode technologyNode)
        {
            return technologyNode?.ParentNode?.Attributes?.First(a => a.OriginalName == "class")?.Value;
        }

        private TechnologyBase GetTechnology(TechnologyType type, IEnumerable<HtmlNode> technologiesNode, Planet planet)
        {
            var technologyNode = GetTechnologyNode(technologiesNode, type);
            var technologyLevel = GetTechnologyLevel(technologyNode);
            var canUpgradeStatus = CanUpgradeStatus(technologyNode);
            var techReached = canUpgradeStatus != "off";
            var canUpgrade = techReached && canUpgradeStatus != "disabled";
            switch (type)
            {
                case TechnologyType.EnergyTechnology:
                    return new EnergyTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.LaserTechnology:
                    return new LaserTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.IonTechnology:
                    return new IonTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.HyperspaceTechnology:
                    return new HyperspaceTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.PlasmaTechnology:
                    return new PlasmaTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.EspionageTechnology:
                    return new EspionageTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.ComputerTechnology:
                    return new ComputerTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.Astrophysics:
                    return new Astrophysics(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.IntergalacticResearchNetwork:
                    return new IntergalacticResearchNetwork(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.GravitonTechnology:
                    return new GravitonTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.WeaponTechnology:
                    return new WeaponTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.ShieldingTechnology:
                    return new ShieldingTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.ArmourTechnology:
                    return new ArmourTechnology(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.CombusionDrive:
                    return new CombustionDrive(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.ImpulseDrive:
                    return new ImpulseDrive(planet, technologyLevel, techReached, canUpgrade);
                case TechnologyType.HyperspaceDrive:
                    return new HyperspaceDrive(planet, technologyLevel, techReached, canUpgrade);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<string> GetUpgradeTechnologyUrl(HtmlDocument document, TechnologyType type)
        {
            return await Task.Run(() =>
            {
                var upgrdeTechnologyHtml =
                    document
                        .DocumentNode
                        .Descendants("div")
                        .First(n => n
                            .Attributes
                            .Any(a => a.Value.Contains($"research{(int) type}")))
                        .Descendants("a")
                        .First(n => n
                            .Attributes
                            .Any(a => a.OriginalName == "onlick"))
                        .GetAttributeValue("onclick", null);

                return Regex.Match(upgrdeTechnologyHtml, @"\'([^']*)\'").Groups[1].Value;
            });
        }

        public async Task<List<TechnologyBase>> GetTechnologies(HtmlDocument document, Planet planet)
        {
            return await Task.Run(() =>
            {
                var planetTechnologies = new List<TechnologyBase>();

                var basicTechnologiesNode = document.GetElementbyId("base1").Descendants("div");
                foreach (var technologyType in BasicTechnologies.List)
                {
                    planetTechnologies.Add(GetTechnology(technologyType, basicTechnologiesNode, planet));
                }

                var driveTechnologiesNode = document.GetElementbyId("base2").Descendants("div");
                foreach (var technologyType in DriveTechnologies.List)
                {
                    planetTechnologies.Add(GetTechnology(technologyType, driveTechnologiesNode, planet));
                }

                var advancedTechnologiesNode = document.GetElementbyId("base3").Descendants("div");
                foreach (var technologyType in AdvancedTechnologies.List)
                {
                  planetTechnologies.Add(GetTechnology(technologyType, advancedTechnologiesNode, planet));  
                }

                var combatTechnologiesNode = document.GetElementbyId("base4").Descendants("div");
                foreach (var technologyType in CombatTechnologies.List)
                {
                    planetTechnologies.Add(GetTechnology(technologyType, combatTechnologiesNode, planet));
                }

                return planetTechnologies;
            });
        } 
    }
}
