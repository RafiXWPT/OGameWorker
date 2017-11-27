using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGameWorker.Code.WorkerQueue
{
    public static class WorkerQueueActionRunner
    {
        private static Task _pending;
        private static readonly object Lock = new object();
        private static bool Ready => _pending == null || _pending.IsCompleted || _pending.IsCanceled || _pending.IsFaulted;

        private static List<QueueActionBase> QueuedActions { get; } = new List<QueueActionBase>();
        public static IEnumerable<QueueActionBase> ExecutionQueue => QueuedActions.Where(a => !a.Completed);

        public static void QueueAction(QueueActionBase action)
        {
            QueuedActions.Add(action);
        }

        public static void RemoveAction(QueueActionBase action)
        {
            QueuedActions.RemoveAll(a => a.ActionId == action.ActionId);
        }

        public static void CheckQueue()
        {
            var actionsToExecute = ExecutionQueue.Where(a => a.CanExecute).OrderBy(a => (int) a.ActionType);
            ExecuteActions(actionsToExecute);
        }

        private static void ExecuteActions(IEnumerable<QueueActionBase> actions)
        {
            foreach (var action in actions)
            {
                Enqueue(action);
                action.Completed = true;
            }
        }

        private static Task Enqueue(QueueActionBase action)
        {
            if (action == null)
                return Task.CompletedTask;

            lock (Lock)
            {
                return _pending = Ready ? action.Execute() : _pending.ContinueWith(_ => action.Execute());
            }
        }
    }
}
