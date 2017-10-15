using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Parser.Planets;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class PlanetProvider
    {
        public PlanetsParser PlanetsParser { get; } = new PlanetsParser();

        public async Task UpdatePlayerPlanets(HtmlDocument document)
        {
            ObjectContainer.Instance.PlayerPlanets = await PlanetsParser.GetPlayerPlanets(document);
        }
    }
}
