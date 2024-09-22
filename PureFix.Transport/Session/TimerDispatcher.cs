using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class TimerDispatcher
    {
        public Task Dispatch(ISessionEventReciever reciever, TimeSpan interval, CancellationToken token)
        {
            var timer = new PeriodicTimer(interval);
            Task task = Task.Factory.StartNew(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await timer.WaitForNextTickAsync(token);
                    reciever.OnTimer();
                }
            },
                TaskCreationOptions.LongRunning);
            return task;
        }
    }
}
