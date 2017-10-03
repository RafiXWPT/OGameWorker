using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Objects.Ships;
using Worker.Objects.Ships.Fighter;
using Worker.Objects.Ships.Other;
using Worker.Objects.Ships.Recycle;
using Worker.Objects.Ships.Transport;

namespace Worker.Parser.Ships
{
    public class ShipsParser
    {
        private HtmlNode GetMilitaryShipNode(IEnumerable<HtmlNode> shipsNode, ShipType type)
        {
            return shipsNode.FirstOrDefault(n => n.HasAttributes &&
                                                 n.Attributes.Any(a => a.Value.Contains($"military{(int) type}")));
        }

        private HtmlNode GetCivilShipNode(IEnumerable<HtmlNode> shipsNode, ShipType type)
        {
            return shipsNode.FirstOrDefault(n => n.HasAttributes &&
                                                 n.Attributes.Any(a => a.Value.Contains($"civil{(int) type}")));
        }

        private int GetShipsCount(HtmlNode shipNode)
        {
            var levelValueString = shipNode
                .Descendants("span")
                .First(n => n.HasAttributes && n.Attributes.Any(a => a.Value.Contains("level")))
                .InnerHtml.Trim();

            return int.TryParse(levelValueString, out int level)
                ? level
                : Convert.ToInt32(levelValueString.Split(new[] {"</span>"}, StringSplitOptions.RemoveEmptyEntries)[1]
                    .Trim());
        }

        private string CanCreateStatus(HtmlNode shipNode)
        {
            return shipNode?.ParentNode?.Attributes?.First(a => a.OriginalName == "class")?.Value;
        }

        private ShipBase GetMilitaryShip(ShipType type, IEnumerable<HtmlNode> shipsNode, Planet planet)
        {
            var shipNode = GetMilitaryShipNode(shipsNode, type);
            var shipsCount = GetShipsCount(shipNode);
            var canCreateStatus = CanCreateStatus(shipNode);
            var techReached = canCreateStatus != "off";
            var canCreate = techReached && canCreateStatus != "disabled";
            switch (type)
            {
                case ShipType.LightFighter:
                    return new LightFighter(planet, shipsCount, techReached, canCreate);
                case ShipType.HeavyFighter:
                    return new HeavyFighter(planet, shipsCount, techReached, canCreate);
                case ShipType.Cruiser:
                    return new Cruiser(planet, shipsCount, techReached, canCreate);
                case ShipType.Battleship:
                    return new Battleship(planet, shipsCount, techReached, canCreate);
                case ShipType.Battlecruiser:
                    return new Battlecruiser(planet, shipsCount, techReached, canCreate);
                case ShipType.Destroyer:
                    return new Destroyer(planet, shipsCount, techReached, canCreate);
                case ShipType.Deathstar:
                    return new Deathstar(planet, shipsCount, techReached, canCreate);
                case ShipType.Bomber:
                    return new Bomber(planet, shipsCount, techReached, canCreate);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private ShipBase GetCivilShip(ShipType type, IEnumerable<HtmlNode> shipsNode, Planet planet)
        {
            var shipNode = GetCivilShipNode(shipsNode, type);
            var shipsCount = GetShipsCount(shipNode);
            var canCreateStatus = CanCreateStatus(shipNode);
            var techReached = canCreateStatus != "off";
            var canCreate = techReached && canCreateStatus != "disabled";
            switch (type)
            {
                case ShipType.SmallCargo:
                    return new SmallCargo(planet, shipsCount, techReached, canCreate);
                case ShipType.LargeCargo:
                    return new LargeCargo(planet, shipsCount, techReached, canCreate);
                case ShipType.Recycler:
                    return new Recycler(planet, shipsCount, techReached, canCreate);
                case ShipType.EspionageProbe:
                    return new EspionageProbe(planet, shipsCount, techReached, canCreate);
                case ShipType.SolarSatellite:
                    return new SolarSatellite(planet, shipsCount, techReached, canCreate);
                case ShipType.ColonyShip:
                    return new ColonyShip(planet, shipsCount, techReached, canCreate);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<List<ShipBase>> GetShips(HtmlDocument document, Planet planet)
        {
            return await Task.Run(() =>
            {
                var planetShips = new List<ShipBase>();
                var militaryShipsNode = document.GetElementbyId("military").Descendants("div");
                foreach (var buildingType in MilitaryShips.List)
                    planetShips.Add(GetMilitaryShip(buildingType, militaryShipsNode, planet));

                var civilShipsNode = document.GetElementbyId("civil").Descendants("div");
                foreach (var buildingType in CivilShips.List)
                    planetShips.Add(GetCivilShip(buildingType, civilShipsNode, planet));

                return planetShips;
            });
        }
    }
}