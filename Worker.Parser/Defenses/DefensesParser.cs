using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Defenses.ActiveDefense;
using Worker.Objects.Structures.Defenses.PassiveDefense;

namespace Worker.Parser.Defenses
{
    public class DefensesParser
    {
        private HtmlNode GetDefenseNode(IEnumerable<HtmlNode> defensesNode, DefenseType type)
        {
            return defensesNode.First(n => n.Descendants("div")
                .Any(d => d.HasAttributes && d.Attributes.Any(a => a.Value.Contains($"defense{(int)type}"))));
        }

        private int GetDefenseAmount(HtmlNode defenseNode)
        {
            int amount;

            var levelValueString = defenseNode
                .Descendants("span")
                .First(s => s.HasAttributes && s.GetAttributeValue("class", null).Contains("level"))
                .InnerHtml.Trim();

            if (int.TryParse(levelValueString, out amount))
                return amount;

            levelValueString = levelValueString.Split(new[] { "</span>" }, StringSplitOptions.RemoveEmptyEntries)[1]
                .Trim();

            if (int.TryParse(levelValueString, out amount))
                return amount;

            return levelValueString.Contains("span")
                ? Convert.ToInt32(levelValueString.Split(new[] { "<span" }, StringSplitOptions.RemoveEmptyEntries)[0]
                    .Trim())
                : 0;
        }

        private string GetBuildStatus(HtmlNode defenseNode)
        {
            return defenseNode.Attributes.First(a => a.OriginalName == "class").Value;
        }

        private DefenseBase GetDefense(IEnumerable<HtmlNode> defensesNode, DefenseType type, Planet planet)
        {
            var defenseNode = GetDefenseNode(defensesNode, type);
            var defenseAmount = GetDefenseAmount(defenseNode);
            var canBuildStatus = GetBuildStatus(defenseNode);
            var techReached = canBuildStatus != "off";
            var canBuild = techReached && canBuildStatus != "disabled";

            switch (type)
            {
                case DefenseType.RocketLauncher:
                    return new RocketLauncher(planet, defenseAmount, techReached, canBuild);
                case DefenseType.LightLaser:
                    return new LightLaser(planet, defenseAmount, techReached, canBuild);
                case DefenseType.HeavyLaser:
                    return new HeavyLaser(planet, defenseAmount, techReached, canBuild);
                case DefenseType.IonCannon:
                    return new IonCannon(planet, defenseAmount, techReached, canBuild);
                case DefenseType.GaussCannon:
                    return new GaussCannon(planet, defenseAmount, techReached, canBuild);
                case DefenseType.PlasmaTurret:
                    return new PlasmaTurret(planet, defenseAmount, techReached, canBuild);
                case DefenseType.SmallShield:
                    return new SmallShield(planet, defenseAmount, techReached, canBuild);
                case DefenseType.LargeShield:
                    return new LargeShield(planet, defenseAmount, techReached, canBuild);
                case DefenseType.AntiBallisticMissile:
                    return null;
                case DefenseType.InterplanetaryMissile:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<List<DefenseBase>> GetDefenses(HtmlDocument document, Planet planet)
        {
            return await Task.Run(() =>
            {
                var planetDefenses = new List<DefenseBase>();

                var defensesNode = document.GetElementbyId("defensebuilding").Descendants("li");
                foreach (var defenseType in Defense.List)
                {
                    planetDefenses.Add(GetDefense(defensesNode, defenseType, planet));
                }

                return planetDefenses;
            });
        }
    }
}
