using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Missions
{
    public class NeutralMission : MissionBase
    {
        public override MovementType MovementType => MovementType.Neutral;

        public NeutralMission(MissionType missionType, DateTime arrivalTime, Planet source, Planet destination, bool isReturning) : base(missionType, arrivalTime, source, destination, isReturning)
        {
        }
    }
}
