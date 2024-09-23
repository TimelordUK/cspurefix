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

        public EventDispatcher(IMessageTransport transport)
        {
            _timerDispatcher = new TimerDispatcher();
            _transportDispatcher = new TransportDispatcher(transport);
        }       

        public async Task Dispatch(CancellationToken token)
        {
            _token = token;
            var tasks = new[] {
                    _timerDispatcher.Dispatch(this, TimeSpan.FromMilliseconds(5000), token),
                    _transportDispatcher.Dispatch(this, token)
                };
            await Task.WhenAny(tasks);
        }

        public void OnRx(byte[] buffer)
        {
            _channel.Writer.WaitToWriteAsync(_token).AsTask().ContinueWith(t =>
            {
                _channel.Writer.TryWrite(new SessionEvent(buffer));
            });
        }

        public void OnTimer()
        {
            _channel.Writer.WaitToWriteAsync(_token).AsTask().ContinueWith(t =>
            {
                _channel.Writer.TryWrite(new SessionEvent());
            });
        }

        public async Task<SessionEvent> WaitRead()
        {
            var next = await _channel.Reader.ReadAsync();
            return next;
        }
    }
}
