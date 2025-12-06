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
        public class AsyncTimer
        {
            private readonly ILogger? m_logger;
            public AsyncTimer(ILogger? logger)
            {
                m_logger = logger;
            }
            public Task Start(TimeSpan interval, Action onInvoke, CancellationToken token)
            {
                var timer = new PeriodicTimer(interval);
                // Use .Unwrap() to properly await the inner async task
                Task task = Task.Factory.StartNew(async () =>
                {
                    m_logger?.Info("timer starting.");
                    while (!token.IsCancellationRequested)
                    {
                        await timer.WaitForNextTickAsync(token);
                        onInvoke.Invoke();
                    }
                    m_logger?.Info("timer exiting.");
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();
                return task;
            }

            public Task Start(TimeSpan interval, Func<Task> onInvoke, CancellationToken token)
            {
                var timer = new PeriodicTimer(interval);
                // Use .Unwrap() to properly await the inner async task
                Task task = Task.Factory.StartNew(async () =>
                {
                    m_logger?.Info("timer starting.");
                    while (!token.IsCancellationRequested)
                    {
                        await timer.WaitForNextTickAsync(token);
                        await onInvoke.Invoke();
                    }
                    m_logger?.Info("timer exiting.");
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();
                return task;
            }

        }
        private readonly ILogger? _logger;
        
        public TimerDispatcher(ILogFactory? factory) {
            _logger = factory?.MakeLogger(nameof(TimerDispatcher));
        }    

        public Task Dispatch(ISessionEventReciever reciever , TimeSpan interval, CancellationToken token)
        {
            var timer = new AsyncTimer(_logger);
            var t = timer.Start(interval, reciever.OnTimer, token);
            return t;
        }
    }
}
