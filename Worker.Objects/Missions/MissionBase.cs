using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Transport = 3,
        Espionage = 6,
        Recycle,
        Colonize
    }

    public abstract class MissionBase
    {
        public abstract MovementType MovementType { get; }
        public MissionType MissionType { get; }
        public DateTime ArrivalTime { get; }
        public Planet Source { get; }
        public Planet Destination { get; }
        public bool IsReturning { get; }

        protected MissionBase(MissionType missionType, DateTime arrivalTime, Planet source, Planet destination, bool isReturning)
        {
            MissionType = missionType;
            ArrivalTime = arrivalTime;
            Source = source;
            Destination = destination;
            IsReturning = isReturning;
        }
    }
}
