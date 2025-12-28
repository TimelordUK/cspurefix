using PureFix.Types;
using System.Threading.Channels;

namespace PureFix.Transport.Session
{
    public class EventDispatcher(ILogFactory? logger, IMessageTransport transport) : ISessionEventReciever
    {
        public readonly record struct SessionEvent(byte[]? Data, int len, bool TransportDead = false);
        private readonly TimerDispatcher _timerDispatcher = new(logger);
        private readonly TransportDispatcher _transportDispatcher = new(logger, transport);
        private readonly Channel<SessionEvent> _channel = Channel.CreateUnbounded<SessionEvent>();
        private CancellationToken _token;
        private readonly ILogger? _logger = logger?.MakeLogger(nameof(EventDispatcher));

        public void Writer(TimeSpan timer, CancellationToken token)
        {
            _token = token;
            // Start dispatchers in background - they run until cancelled
            // These fire-and-forget tasks write to the channel that Reader consumes
            // We don't await them - they run in background while Reader consumes from channel
            _ = _timerDispatcher.Dispatch(this, timer, token);
            _ = _transportDispatcher.Dispatch(this, token);
            _logger?.Info("Writer started timer and transport dispatchers.");
        }

        public void OnRx(byte[] buffer, int len)
        {
            // Channel is thread-safe, no need to serialize through queue
            _channel.Writer.TryWrite(new SessionEvent(buffer, len));
        }

        public void OnTimer()
        {
            // Channel is thread-safe, no need to serialize through queue
            _channel.Writer.TryWrite(new SessionEvent());
        }

        public void OnTransportDead()
        {
            // Signal that transport has died - session should stop
            _logger?.Info("Transport dead, signaling session to stop.");
            _channel.Writer.TryWrite(new SessionEvent(null, 0, TransportDead: true));
        }

        public async Task<SessionEvent> WaitRead()
        {
            var next = await _channel.Reader.ReadAsync(_token);
            return next;
        }
    }
}
