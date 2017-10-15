using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Galaxy;
using Worker.Parser.Resources;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class ResourcesProvider
    {
        public ResourcesParser ResourcesParser { get; } = new ResourcesParser();

        public async Task UpdatePlanetResources(HtmlDocument document, Planet planet)
        {
            await ResourcesParser.GetPlanetResources(planet, document);
        }
    }
}
