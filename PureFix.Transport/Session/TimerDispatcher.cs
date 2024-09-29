using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class TimerDispatcher
    {
        private ILogger? _logger;
        public TimerDispatcher(ILogFactory? factory) {
            _logger = factory?.MakeLogger(nameof(TimerDispatcher));
        }    
        public Task Dispatch(ISessionEventReciever reciever , TimeSpan interval, CancellationToken token)
        {
            var timer = new PeriodicTimer(interval);
            Task task = Task.Factory.StartNew(async () =>
            {
                _logger?.Info("timer starting.");
                while (!token.IsCancellationRequested)
                {
                    await timer.WaitForNextTickAsync(token);
                    reciever.OnTimer();
                }
                _logger?.Info("timer exiting.");
            },
                TaskCreationOptions.LongRunning);
            return task;
        }
    }
}
