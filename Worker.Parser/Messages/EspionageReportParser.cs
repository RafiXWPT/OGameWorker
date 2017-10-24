using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Messages;
using Worker.Objects.Resources;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Buildings.Resource;
using Worker.Objects.Structures.Buildings.Station;
using Worker.Objects.Structures.Buildings.Warehouse;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Defenses.ActiveDefense;
using Worker.Objects.Structures.Defenses.PassiveDefense;
using Worker.Objects.Structures.Ships;
using Worker.Objects.Structures.Ships.Fighter;
using Worker.Objects.Structures.Ships.Other;
using Worker.Objects.Structures.Ships.Recycle;
using Worker.Objects.Structures.Ships.Transport;

namespace Worker.Parser.Messages
{
    public class EspionageReportParser
    {
        public async Task<EspionageReport> ReadEspionageReport(MessageBase message, HtmlDocument document)
        {
            return await Task.Run(() =>
            {
                var report = message as EspionageReport;
                if (report == null)
                    return null;

                FillReportResources(report, document);
                FillReportBuildings(report, document);
                FillReportFleet(report, document);
                FillReportDefenses(report, document);

                return report;
            });
        }

        private void FillReportResources(EspionageReport report, HtmlDocument document)
        {
            var resourceNode = document.DocumentNode.Descendants("ul").First(d => d.GetAttributeValue("data-type", null) == "resources");
            foreach (var node in resourceNode.Descendants("li"))
            {
                var nodeClassValue = node.Descendants("div").First().GetAttributeValue("class", null);
                var nodeResourceValue = node.Descendants("span").First().InnerText.Replace(".", string.Empty);
                if (nodeClassValue.Contains("metal"))
                {
                    report.Metal = new Metal(nodeResourceValue);
                }
                else if (nodeClassValue.Contains("crystal"))
                {
                    report.Crystal = new Crystal(nodeResourceValue);
                }
                else if (nodeClassValue.Contains("deuterium"))
                {
                    report.Deuterium = new Deuterium(nodeResourceValue);
                }
                else if (nodeClassValue.Contains("energy"))
                {
                    report.Energy = new Energy(nodeResourceValue);
                }
            }
        }

        private void FillReportFleet(EspionageReport report, HtmlDocument document)
        {
            var fleetNode = document.DocumentNode.Descendants("ul").First(d => d.GetAttributeValue("data-type", null) == "ships");
            var ships = new List<ShipBase>();
            foreach (var node in fleetNode.Descendants("li"))
            {
                var shipType = (ShipType)Convert.ToInt32(node.Descendants("img")
                    .First(i => i.GetAttributeValue("class", null).Contains("tech"))
                    .GetAttributeValue("class", null)
                    .Replace("tech", string.Empty));
                var amount = Convert.ToInt32(node.Descendants("span")
                    .First(i => i.GetAttributeValue("class", null) == "fright")
                    .InnerText);

                switch (shipType)
                {
                    case ShipType.SmallCargo:
                        ships.Add(new SmallCargo(report.Target, amount, true, false));
                        break;
                    case ShipType.LargeCargo:
                        ships.Add(new LargeCargo(report.Target, amount, true, false));
                        break;
                    case ShipType.LightFighter:
                        ships.Add(new LightFighter(report.Target, amount, true, false));
                        break;
                    case ShipType.HeavyFighter:
                        ships.Add(new HeavyFighter(report.Target, amount, true, false));
                        break;
                    case ShipType.Cruiser:
                        ships.Add(new Cruiser(report.Target, amount, true, false));
                        break;
                    case ShipType.Battleship:
                        ships.Add(new Battleship(report.Target, amount, true, false));
                        break;
                    case ShipType.Battlecruiser:
                        ships.Add(new Battlecruiser(report.Target, amount, true, false));
                        break;
                    case ShipType.Destroyer:
                        ships.Add(new Destroyer(report.Target, amount, true, false));
                        break;
                    case ShipType.Deathstar:
                        ships.Add(new Deathstar(report.Target, amount, true, false));
                        break;
                    case ShipType.Bomber:
                        ships.Add(new Bomber(report.Target, amount, true, false));
                        break;
                    case ShipType.Recycler:
                        ships.Add(new Recycler(report.Target, amount, true, false));
                        break;
                    case ShipType.EspionageProbe:
                        ships.Add(new EspionageProbe(report.Target, amount, true, false));
                        break;
                    case ShipType.SolarSatellite:
                        ships.Add(new SolarSatellite(report.Target, amount, true, false));
                        break;
                    case ShipType.ColonyShip:
                        ships.Add(new ColonyShip(report.Target, amount, true, false));
                        break;
                }
            }

            report.Ships = ships;
        }

        public void FillReportDefenses(EspionageReport report, HtmlDocument document)
        {
            var fleetNode = document.DocumentNode.Descendants("ul").First(d => d.GetAttributeValue("data-type", null) == "defense");
            var defenses = new List<DefenseBase>();
            foreach (var node in fleetNode.Descendants("li"))
            {
                var defenseType = (DefenseType)Convert.ToInt32(node.Descendants("img")
                    .First(i => i.GetAttributeValue("class", null).Contains("defense"))
                    .GetAttributeValue("class", null)
                    .Replace("defense", string.Empty));
                var amount = Convert.ToInt32(node.Descendants("span")
                    .First(i => i.GetAttributeValue("class", null) == "fright")
                    .InnerText);

                switch (defenseType)
                {
                    case DefenseType.RocketLauncher:
                        defenses.Add(new RocketLauncher(report.Target, amount, true, false));
                        break;
                    case DefenseType.LightLaser:
                        defenses.Add(new LightLaser(report.Target, amount, true, false));
                        break;
                    case DefenseType.HeavyLaser:
                        defenses.Add(new HeavyLaser(report.Target, amount, true, false));
                        break;
                    case DefenseType.IonCannon:
                        defenses.Add(new IonCannon(report.Target, amount, true, false));
                        break;
                    case DefenseType.GaussCannon:
                        defenses.Add(new GaussCannon(report.Target, amount, true, false));
                        break;
                    case DefenseType.PlasmaTurret:
                        defenses.Add(new PlasmaTurret(report.Target, amount, true, false));
                        break;
                    case DefenseType.SmallShield:
                        defenses.Add(new SmallShield(report.Target, amount, true, false));
                        break;
                    case DefenseType.LargeShield:
                        defenses.Add(new LargeShield(report.Target, amount, true, false));
                        break;
                }
            }

            report.Defenses = defenses;
        }

        private void FillReportBuildings(EspionageReport report, HtmlDocument document)
        {
            //buildings
            var fleetNode = document.DocumentNode.Descendants("ul").First(d => d.GetAttributeValue("data-type", null) == "buildings");
            var buildings = new List<BuildingBase>();
            foreach (var node in fleetNode.Descendants("li"))
            {
                var buildingType = (BuildingType)Convert.ToInt32(node.Descendants("img")
                    .First(i => i.GetAttributeValue("class", null).Contains("building"))
                    .GetAttributeValue("class", null)
                    .Replace("building", string.Empty));
                var level = Convert.ToInt32(node.Descendants("span")
                    .First(i => i.GetAttributeValue("class", null) == "fright")
                    .InnerText);

                switch (buildingType)
                {
                    case BuildingType.MetalMine:
                        buildings.Add(new MetalMine(report.Target, level, true, false));
                        break;
                    case BuildingType.CrystalMine:
                        buildings.Add(new CrystalMine(report.Target, level, true, false));
                        break;
                    case BuildingType.DeuteriumExtractor:
                        buildings.Add(new DeuteriumExtractor(report.Target, level, true, false));
                        break;
                    case BuildingType.SolarPowerPlant:
                        buildings.Add(new SolarPowerPlant(report.Target, level, true, false));
                        break;
                    case BuildingType.FusionReactor:
                        buildings.Add(new FusionReactor(report.Target, level, true, false));
                        break;
                    case BuildingType.MetalStorage:
                        buildings.Add(new MetalStorage(report.Target, level, true, false));
                        break;
                    case BuildingType.CrystalStorage:
                        buildings.Add(new CrystalStorage(report.Target, level, true, false));
                        break;
                    case BuildingType.DeuteriumTank:
                        buildings.Add(new DeuteriumTank(report.Target, level, true, false));
                        break;
                    case BuildingType.RoboticsFactory:
                        buildings.Add(new RoboticsFactory(report.Target, level, true, false));
                        break;
                    case BuildingType.Shipyard:
                        buildings.Add(new Shipyard(report.Target, level, true, false));
                        break;
                    case BuildingType.ResearchLabolatory:
                        buildings.Add(new ResearchLabolatory(report.Target, level, true, false));
                        break;
                    case BuildingType.RocketSilo:
                        buildings.Add(new RocketSilo(report.Target, level, true, false));
                        break;
                    case BuildingType.NaniteFactory:
                        buildings.Add(new NaniteFactory(report.Target, level, true, false));
                        break;
                }
            }

            report.Buildings = buildings;
        }
    }
}
