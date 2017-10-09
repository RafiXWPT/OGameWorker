using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGameWorker.Code.WorkerQueue
{
    public class QueueAction
    {
        public int MissionId { get; set; }
        public Func<Task> Action { get; set; }
        public DateTime ExecutionTime { get; set; }
        public bool Completed { get; set; }
    }
}
