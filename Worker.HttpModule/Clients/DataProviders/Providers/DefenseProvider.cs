using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;
using Worker.Parser.Defenses;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class DefenseProvider
    {
        public DefensesParser DefenseParser { get; } = new DefensesParser();

        public async Task UpdatePlanetDefense(HtmlDocument document, PlayerPlanet planet)
        {
            ObjectContainer.Instance.PlayerDefense.RemoveAll(d => d.BelongsTo.Id == planet.Id);
            var planetDefense = await DefenseParser.GetDefenses(document, planet);
            ObjectContainer.Instance.PlayerDefense.AddRange(planetDefense);
        }
    }
}
