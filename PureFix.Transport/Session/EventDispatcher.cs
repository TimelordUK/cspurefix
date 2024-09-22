using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class EventDispatcher
    {
        private readonly TimerDispatcher _timerDispatcher;
        private readonly TransportDispatcher _transportDispatcher;

        public EventDispatcher(IMessageTransport transport)
        {
            _timerDispatcher = new TimerDispatcher();
            _transportDispatcher = new TransportDispatcher(transport);
        }

        public Task Dispatch(ISessionEventReciever receiver, CancellationToken token)
        {
            var tasks = new[] {
                    _timerDispatcher.Dispatch(receiver, TimeSpan.FromMilliseconds(1), token),
                    _transportDispatcher.Dispatch(receiver, token)
                };
            return Task.WhenAny(tasks);
        }
    }
}
