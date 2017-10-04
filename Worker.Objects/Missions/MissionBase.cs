using System;
using Worker.Objects.Galaxy;

namespace Worker.Objects.Missions
{
    public enum MovementType
    {
        Friendly,
        Neutral,
        Hostile
    }

    public enum MissionType
    {
        Attack = 1,
        GuildAttack = 2,
        Transport = 3,
        Stationize = 4,
        Stop = 5,
        Espionage = 6,
        Colonize = 7,
        Recycle = 8,
        Destroy = 9,
        Expedition = 15
    }

    public abstract class MissionBase
    {
        protected MissionBase(int missionId, MissionType missionType, DateTime arrivalTime, Planet source, Planet destination, bool isReturning)
        {
            MissionId = missionId;
            MissionType = missionType;
            ArrivalTime = arrivalTime;
            Source = source;
            Destination = destination;
            IsReturning = isReturning;
        }

        public int MissionId { get; }
        public abstract MovementType MovementType { get; }
        public MissionType MissionType { get; }
        public DateTime ArrivalTime { get; }
        public Planet Source { get; }
        public Planet Destination { get; }
        public bool IsReturning { get; }
    }
}