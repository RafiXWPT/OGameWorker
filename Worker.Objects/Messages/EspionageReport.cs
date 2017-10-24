using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;
using Worker.Objects.Resources;

namespace Worker.Objects.Messages
{
    public class EspionageReport : MessageBase
    {
        public EnemyPlanet Target { get; set; }
        public Metal Metal { get; set; }
        public Crystal Crystal { get; set; }
        public Deuterium Deuterium { get; set; }

        public EspionageReport(int messageId, DateTime messageDate, EnemyPlanet target) : base(messageId, messageDate, MessageType.Espionage, MessageStatus.New)
        {
            Target = target;
        }
    }
}
