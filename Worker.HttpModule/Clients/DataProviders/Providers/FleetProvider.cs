using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Parser.Ships;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class FleetProvider
    {
        public FleetParser FleetParser { get; } = new FleetParser();

        public async Task UpdatePlanetFleet(HtmlDocument document, Planet planet)
        {
            ObjectContainer.Instance.PlayerFleet.RemoveAll(s => s.BelongsTo.Id == planet.Id);
            var planetShips = await FleetParser.GetFleet(document, planet);
            ObjectContainer.Instance.PlayerFleet.AddRange(planetShips);
        }
    }
}
