using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Resources;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Ships;

namespace Worker.Objects.Messages
{
    public class EspionageReport : MessageBase
    {
        public EnemyPlanet Target { get; set; }
        public Metal Metal { get; set; }
        public Crystal Crystal { get; set; }
        public Energy Energy { get; set; }
        public Deuterium Deuterium { get; set; }
        public List<BuildingBase> Buildings { get; set; } = new List<BuildingBase>();
        public List<DefenseBase> Defenses { get; set; } = new List<DefenseBase>();
        public List<ShipBase> Ships { get; set; } = new List<ShipBase>();

        public EspionageReport(int messageId, DateTime messageDate, EnemyPlanet target) : base(messageId, messageDate, MessageType.Espionage, MessageStatus.New)
        {
            Target = target;
        }
    }
}
