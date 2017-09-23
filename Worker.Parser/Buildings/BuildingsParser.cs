using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Buildings;
using Worker.Objects.Galaxy;

namespace Worker.Parser.Buildings
{
    public class BuildingsParser
    {
        public async Task<List<BuildingBase>> GetResourceBuildings(HtmlDocument document, Planet planet)
        {
            var planetResourceBuildings = await Task.Run(() =>
            {
                return "";
            });

            return new List<BuildingBase>();
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
