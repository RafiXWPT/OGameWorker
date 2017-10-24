using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Messages;
using Worker.Parser.Messages;

namespace Worker.HttpModule.Clients.DataProviders.Providers
{
    public class MessagesProvider
    {
        private readonly SemaphoreSlim _lockObject = new SemaphoreSlim(1);
        public MessagesParser MessagesParser { get; } = new MessagesParser();

        public async Task GetNewMessages(HtmlDocument document, MessageType type)
        {
            await _lockObject.WaitAsync();
            var newMessages = await MessagesParser.GetNewMessages(document, type);
            ObjectContainer.Instance.Messages.AddRange(newMessages);
            _lockObject.Release();
        }

        public async Task ReadMessage(HtmlDocument document, MessageBase messageWrapper)
        {
            var updatedMessage = await MessagesParser.ReadMessage(document, messageWrapper);
            switch (messageWrapper.MessageType)
            {
                case MessageType.Espionage:
                    var espionageReport = updatedMessage as EspionageReport;
                    if (espionageReport == null)
                        return;

                    var planetToUpdate = ObjectContainer.Instance.GetGalaxyPlanet<EnemyPlanet>(espionageReport.Target.Id);
                    planetToUpdate.Metal = espionageReport.Metal;
                    planetToUpdate.Crystal = espionageReport.Crystal;
                    planetToUpdate.Deuterium = espionageReport.Deuterium;
                    planetToUpdate.ScanStatus = ScanStatus.Scanned;
                    messageWrapper.MessageStatus = MessageStatus.Readed;

                    return;
                case MessageType.WarRepor:
                    return;
                case MessageType.Transport:
                    return;
                case MessageType.Other:
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageWrapper.MessageType), messageWrapper.MessageType, null);
            }

        }
    }
}
