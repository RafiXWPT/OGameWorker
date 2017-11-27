using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Structures;
using Worker.Objects.Structures.Buildings;

namespace OGameWorker.Code.WorkerQueue
{
    public enum ActionType
    {
        TimeBase = 0,
        ConditionBase = 1,
        Other = 2
    }

    public enum ActionTarget
    {
        FleetSave,
        ReturnFleet,
        ReadEspionageReport,
        UpgradeBuilding
    }

    public abstract class QueueActionBase
    {
        public abstract int ActionId { get; }
        public abstract ActionTarget ActionTarget { get; }
        public abstract ActionType ActionType { get; }

        public bool Completed { get; set; }
        public abstract bool CanExecute { get; }
        public abstract Task Execute();
    }

    public class TimeExecutionAction : QueueActionBase
    {
        public override int ActionId { get; }
        public override ActionTarget ActionTarget { get; }
        public override ActionType ActionType => ActionType.TimeBase;

        public Func<Task> Action { get; set; }
        public DateTime ExecutionTime { get; set; }

        public TimeExecutionAction(ActionTarget actionTarget) : this(new Random().Next(int.MaxValue), actionTarget) { }

        public TimeExecutionAction(int actionId, ActionTarget actionTarget)
        {
            ActionId = actionId;
            ActionTarget = actionTarget;        
        }

        public override bool CanExecute => DateTime.Now <= ExecutionTime;

        public override Task Execute()
        {
            return Task.Run(Action);
        }
    }

    public class ConditionExecutionAction : QueueActionBase
    {
        public override int ActionId { get; }
        public override ActionTarget ActionTarget { get; }
        public override ActionType ActionType => ActionType.ConditionBase;

        public Func<Task> Action { get; set; }
        public Func<bool> Condition { get; set; }

        public ConditionExecutionAction(ActionTarget actionTarget) : this(new Random().Next(int.MaxValue), actionTarget) { }

        public ConditionExecutionAction(int actionId, ActionTarget actionTarget)
        {
            ActionId = actionId;
            ActionTarget = actionTarget;
        }

        public override bool CanExecute => Condition();
        public override Task Execute()
        {
            return Task.Run(Action);
        }
    }

    public class UpgradeBuildingAction : QueueActionBase
    {
        public override int ActionId { get; } = new Random().Next(int.MaxValue);
        public override ActionTarget ActionTarget => ActionTarget.UpgradeBuilding;
        public override ActionType ActionType => ActionType.Other;

        public int PlanetId { get; set; }
        public BuildingType BuildingType { get; set; }
        public int CurrentUpgradeOrder => ObjectContainer.Instance.GetPlayerPlanet(PlanetId).BuildingQueue.FindIndex(p => p.Type == BuildingType);

        public override bool CanExecute => CurrentUpgradeOrder == 1 && ObjectContainer.Instance.GetBuilding(PlanetId, BuildingType).CanBuild;
        public override Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
