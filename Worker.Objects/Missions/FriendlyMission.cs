using System;
using System.Linq;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Missions
{
    public class FriendlyMission : MissionBase
    {
        public FriendlyMission(int missionId, MissionType missionType, int arrivalTimestamp, Planet source, Planet destination, bool isReturning) 
            : base(missionId, missionType, arrivalTimestamp, source, destination, isReturning)
        {
        }

        public override MovementType MovementType => MovementType.Friendly;
    }
}