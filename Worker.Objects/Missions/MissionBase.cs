using System;
using System.Linq;
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
        protected MissionBase(int missionId, MissionType missionType, int arrivalTimestamp, Planet source, Planet destination, bool isReturning)
        {
            MissionId = missionId;
            MissionType = missionType;
            ArrivalTimestamp = arrivalTimestamp;
            Source = source;
            Destination = destination;
            IsReturning = isReturning;
        }

        public int MissionId { get; }
        public abstract MovementType MovementType { get; }
        public MissionType MissionType { get; }
        public int ArrivalTimestamp { get; }
        public DateTime ArrivalTime => new DateTime(1970, 1, 1, 0, 0, 0, 0).AddHours(2).AddSeconds(ArrivalTimestamp);
        public Planet Source { get; }
        public int? SourceId => ObjectContainer.Instance.PlayerPlanets.FirstOrDefault(p => p.Name == Source.Name)?.Id;
        public Planet Destination { get; }
        public int? DestinationId => ObjectContainer.Instance.PlayerPlanets.FirstOrDefault(p => p.Name == Destination.Name)?.Id;
        public bool IsReturning { get; }
    }
}