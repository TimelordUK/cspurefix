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
                    try
                    {
                        while (!token.IsCancellationRequested)
                        {
                            var ticked = await timer.WaitForNextTickAsync(token);
                            if (!ticked)
                            {
                                logger?.Info("timer WaitForNextTickAsync returned false, exiting.");
                                break;
                            }
                            onInvoke.Invoke();
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        logger?.Info("timer cancelled.");
                    }
                    catch (Exception ex)
                    {
                        logger?.Error(ex, "timer unexpected exception: {Message}", ex.Message);
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
                    try
                    {
                        while (!token.IsCancellationRequested)
                        {
                            var ticked = await timer.WaitForNextTickAsync(token);
                            if (!ticked)
                            {
                                logger?.Info("timer WaitForNextTickAsync returned false, exiting.");
                                break;
                            }
                            await onInvoke.Invoke();
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        logger?.Info("timer cancelled.");
                    }
                    catch (Exception ex)
                    {
                        logger?.Error(ex, "timer unexpected exception: {Message}", ex.Message);
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
