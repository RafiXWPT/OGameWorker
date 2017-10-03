﻿using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Missions
{
    public class FriendlyMission : MissionBase
    {
        public FriendlyMission(MissionType missionType, DateTime arrivalTime, Planet source, Planet destination,
            bool isReturning) : base(missionType, arrivalTime, source, destination, isReturning)
        {
        }

        public override MovementType MovementType => MovementType.Friendly;
    }
}