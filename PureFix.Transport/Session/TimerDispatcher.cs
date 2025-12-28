using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class TimerDispatcher(ILogFactory? factory)
    {
        public class AsyncTimer(ILogger? logger)
        {
            public Task Start(TimeSpan interval, Action onInvoke, CancellationToken token)
            {
                var timer = new PeriodicTimer(interval);
                // Use .Unwrap() to properly await the inner async task
                var task = Task.Factory.StartNew(async () =>
                {
                    logger?.Info("timer starting.");
                    while (!token.IsCancellationRequested)
                    {
                        await timer.WaitForNextTickAsync(token);
                        onInvoke.Invoke();
                    }
                    logger?.Info("timer exiting.");
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();
                return task;
            }

            public Task Start(TimeSpan interval, Func<Task> onInvoke, CancellationToken token)
            {
                var timer = new PeriodicTimer(interval);
                // Use .Unwrap() to properly await the inner async task
                var task = Task.Factory.StartNew(async () =>
                {
                    logger?.Info("timer starting.");
                    while (!token.IsCancellationRequested)
                    {
                        await timer.WaitForNextTickAsync(token);
                        await onInvoke.Invoke();
                    }
                    logger?.Info("timer exiting.");
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();
                return task;
            }

        }
        private readonly ILogger? _logger = factory?.MakeLogger(nameof(TimerDispatcher));

        public Task Dispatch(ISessionEventReciever reciever , TimeSpan interval, CancellationToken token)
        {
            var timer = new AsyncTimer(_logger);
            var t = timer.Start(interval, reciever.OnTimer, token);
            return t;
        }
    }
}
