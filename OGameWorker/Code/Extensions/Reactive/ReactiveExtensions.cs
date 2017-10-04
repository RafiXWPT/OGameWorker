using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGameWorker.Code.Extensions.Reactive
{
    public static class ReactiveExtensions
    {
        public static IObservable<long> DelayTask(this IObservable<long> observable, Func<TimeSpan> delay)
        {
            return observable.Do(t => { Task.Delay(delay()).GetAwaiter().GetResult(); });
        }
    }
}
