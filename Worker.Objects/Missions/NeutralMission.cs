using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Missions
{
    public class NeutralMission : MissionBase
    {
        public NeutralMission(int missionId, MissionType missionType, int arrivalTimestamp, Planet source, Planet destination,
            bool isReturning) : base(missionId, missionType, arrivalTimestamp, source, destination, isReturning)
        {
        }

        public override MovementType MovementType => MovementType.Neutral;
    }
}