using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Parser.Galaxy;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class GalaxyProvider
    {
        public GalaxyParser GalaxyParser { get; } = new GalaxyParser();

        public async Task<List<Planet>> ReadGalaxyPlanets(HtmlDocument document, int galaxy, int system)
        {
            return await GalaxyParser.GetPlanets(document, galaxy, system);
        }
    }
}
