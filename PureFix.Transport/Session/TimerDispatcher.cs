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
            public Task Start(TimeSpan interval, CancellationToken token, Action onInvoke)
            {
                var timer = new PeriodicTimer(interval);
                Task task = Task.Factory.StartNew(async () =>
                {
                    m_logger?.Info("timer starting.");
                    while (!token.IsCancellationRequested)
                    {
                        await timer.WaitForNextTickAsync(token);
                        onInvoke.Invoke();
                    }
                    m_logger?.Info("timer exiting.");
                },
                    TaskCreationOptions.LongRunning);
                return task;
            }

            public Task Start(TimeSpan interval, CancellationToken token, Func<Task> onInvoke)
            {
                var timer = new PeriodicTimer(interval);
                Task task = Task.Factory.StartNew(async () =>
                {
                    m_logger?.Info("timer starting.");
                    while (!token.IsCancellationRequested)
                    {
                        await timer.WaitForNextTickAsync(token);
                        await onInvoke.Invoke();
                    }
                    m_logger?.Info("timer exiting.");
                },
                    TaskCreationOptions.LongRunning);
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
            var t = timer.Start(interval, token, reciever.OnTimer);
            return t;
        }
    }
}
