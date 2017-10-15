using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Parser.Movement;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class MissionsProvider
    {
        public MissionsParser MissionsParser { get; } = new MissionsParser();

        public async Task UpdateMissions(HtmlDocument document)
        {
            ObjectContainer.Instance.Missions = await MissionsParser.GetMissions(document);
        }
    }
}
