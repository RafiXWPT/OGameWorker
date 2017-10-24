using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;
using Worker.Parser.Technologies;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class TechnologiesProvider
    {
        public TechnologiesParser TechnologiesParser { get; } = new TechnologiesParser();

        public async Task UpdatePlanetTechnologies(HtmlDocument document, PlayerPlanet planet)
        {
            ObjectContainer.Instance.PlayerTechnologies.RemoveAll(t => t.BelongsTo.Id == planet.Id);
            var planetTechnologies = await TechnologiesParser.GetTechnologies(document, planet);
            ObjectContainer.Instance.PlayerTechnologies.AddRange(planetTechnologies);
        }
    }
}
