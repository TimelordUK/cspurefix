using Arrow.Threading.Tasks;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class EventDispatcher : ISessionEventReciever
    {
        public readonly record struct SessionEvent(byte[]? Data = null);
        private readonly TimerDispatcher _timerDispatcher;
        private readonly TransportDispatcher _transportDispatcher;
        private readonly Channel<SessionEvent> _channel = Channel.CreateUnbounded<SessionEvent>();
        private CancellationToken _token;
        private readonly ILogger? _logger;
        private readonly AsyncWorkQueue _q;

        public EventDispatcher(ILogFactory? logger, AsyncWorkQueue q, IMessageTransport transport)
        {
            _q = q;
            _logger = logger?.MakeLogger(nameof(EventDispatcher));
            _timerDispatcher = new TimerDispatcher(logger);
            _transportDispatcher = new TransportDispatcher(transport);
        }       

        public async Task Writer(TimeSpan timer, CancellationToken token)
        {
            await Task.Factory.StartNew(async () =>
            {
                _token = token;
                var tasks = new[] {
                    _timerDispatcher.Dispatch(this, timer, token),
                    _transportDispatcher.Dispatch(this, token)
                };
                _logger?.Info("Writer is waiting on events.");
                await Task.WhenAny(tasks);
                _logger?.Info("Writer has completed.");
            }, TaskCreationOptions.LongRunning);
        }

        public void OnRx(byte[] buffer)
        {
            _q.EnqueueAsync(() =>
            {
                _channel.Writer.TryWrite(new SessionEvent(buffer));
            });
        }

        public void OnTimer()
        {
            _q.EnqueueAsync(() =>
            {
                _channel.Writer.TryWrite(new SessionEvent());
            });
        }

        public async Task<SessionEvent> WaitRead()
        {
            var next = await _channel.Reader.ReadAsync(_token);
            return next;
        }
    }
}
