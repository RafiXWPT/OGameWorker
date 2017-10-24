using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects;
using Worker.Objects.Galaxy;
using Worker.Objects.Messages;
using Worker.Objects.Resources;

namespace Worker.Parser.Messages
{
    public class MessagesParser
    {
        private Planet GetPlanetOnPosition(Position position)
        {
            return ObjectContainer.Instance.GalaxyPlanets.FirstOrDefault(
                p => p.Position.Galaxy == position.Galaxy && p.Position.System == position.System &&
                     p.Position.Planet == position.Planet);
        }

        private async Task<MessageBase> GetMessage(HtmlNode messageNode, MessageType type)
        {
            return await Task.Run(() =>
            {
                var messageId = Convert.ToInt32(messageNode.GetAttributeValue("data-msg-id", null));
                if (ObjectContainer.Instance.Messages.Any(m => m.MessageId == messageId))
                    return null;

                var messageDate = DateTime.Parse(messageNode.Descendants("span").First(s => s.GetAttributeValue("class", null).Contains("msg_date")).InnerText);
                var messagePlanetNamePosition = messageNode.Descendants("a").First().InnerText.Split('[');
                var messageTargetPositionParts = messagePlanetNamePosition[1].Replace("]", string.Empty).Split(':');
                var messageTargetPosition = new Position()
                {
                    Galaxy = Convert.ToInt32(messageTargetPositionParts[0]),
                    System = Convert.ToInt32(messageTargetPositionParts[1]),
                    Planet = Convert.ToInt32(messageTargetPositionParts[2])
                };

                var messageTargetPlanet = GetPlanetOnPosition(messageTargetPosition);

                switch (type)
                {
                    case MessageType.Espionage:
                        return new EspionageReport(messageId, messageDate, messageTargetPlanet as EnemyPlanet);
                    case MessageType.WarRepor:
                        return null;
                    case MessageType.Transport:
                        return null;
                    case MessageType.Other:
                        return null;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            });
        }

        private async Task<EspionageReport> ReadEspionageReport(HtmlDocument document, MessageBase message)
        {
            return await Task.Run(() =>
            {
                var report = message as EspionageReport;
                if (report == null)
                    return null;

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
                    }
                }

                return report;
            });
        }

        public async Task<int> GetMessageMaxPages(HtmlDocument document)
        {
            return await Task.Run(() =>
            {
                var pageNode = document.DocumentNode.Descendants("ul")
                    .First(u => u.GetAttributeValue("class", null) == "pagination");

                return Convert.ToInt32(pageNode.Descendants("li")
                    .First(l => l.GetAttributeValue("class", null) == "curPage")
                    .InnerText.Split('/')[1]);
            });
        }

        public async Task<List<MessageBase>> GetNewMessages(HtmlDocument document, MessageType type)
        {
            return await Task.Run(async () =>
            {
                var messages = new List<MessageBase>();
                var messagesNode = document.GetElementbyId("fleetsgenericpage");
                var newMessages = messagesNode.Descendants("li").Where(d => d.HasAttributes && d.Attributes.Any(a => a.Value.Contains("msg")));
                foreach (var newMessage in newMessages)
                {
                    var message = await GetMessage(newMessage, type);
                    if(message == null)
                        continue;
                    
                    messages.Add(message);
                }
                return messages;
            });
        }

        public async Task<MessageBase> ReadMessage(HtmlDocument document, MessageBase message)
        {
            switch (message.MessageType)
            {
                case MessageType.Espionage:
                    return await ReadEspionageReport(document, message);
                case MessageType.WarRepor:
                    return null;
                case MessageType.Transport:
                    return null;
                case MessageType.Other:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
