using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGameWorker.Code.WorkerQueue
{
    public class WorkerQueue
    {
        public List<QueueAction> QueueActions;
        protected Task Pending;
        public bool Ready => Pending == null || Pending.IsCompleted || Pending.IsCanceled || Pending.IsFaulted;

        public void QueueAction(QueueAction action)
        {
            QueueActions.Add(action);
        }

        public void ExecuteByTime()
        {
            var actionsToRun = QueueActions.Where(a => a.ExecutionTime < DateTime.Now && !a.Completed);
            foreach (var action in actionsToRun)
            {
                Enqueue(action);
                action.Completed = true;
            }
        }

        public Task Enqueue(QueueAction action)
        {
            if (action == null)
                return Task.CompletedTask;

            lock (this)
            {
                return Pending = Ready ? Task.Factory.StartNew(action.Action) : Pending.ContinueWith(_ => action.Action());
            }
        }
    }
}
