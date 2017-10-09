using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGameWorker.Code.WorkerQueue
{
    public static class WorkerQueue
    {
        private static readonly object Lock = new object();
        public static List<QueueAction> QueueActions = new List<QueueAction>();
        private static Task _pending;
        public static bool Ready => _pending == null || _pending.IsCompleted || _pending.IsCanceled || _pending.IsFaulted;

        public static void QueueAction(QueueAction action)
        {
            QueueActions.Add(action);
        }

        public static void CheckQueue()
        {
            var actionsToRun = QueueActions.Where(a => a.ExecutionTime < DateTime.Now && !a.Completed);
            foreach (var action in actionsToRun)
            {
                Enqueue(action);
                action.Completed = true;
            }
        }

        public static Task Enqueue(QueueAction action)
        {
            if (action == null)
                return Task.CompletedTask;

            lock (Lock)
            {
                return _pending = Ready ? Task.Run(action.Action) : _pending.ContinueWith(_ => action.Action());
            }
        }
    }
}
