using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.Transport.Ascii
{
    public abstract class AsciiSession : FixSession
    {
        protected AsciiSession(IFixConfig config, IMessageTransport transport, IMessageParser parser, IMessageEncoder encoder, IFixClock clock) : base(config, transport, parser, encoder, clock)
        {
        }

        private async Task SendTestRequest()
        {
            SetState(SessionState.AwaitingProcessingResponseToTestRequest);
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var text = $"test-req-{now}";
            var tr = m_factory?.TestRequest(text);
            if (tr != null)
            {
                await Send(MsgType.TestRequest, tr);
            }
        }

        private async Task SendHeartbeat()
        {
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var text = $"heartbeat-{now}";
            var tr = m_factory?.Heartbeat(text);
            if (tr != null)
            {
                await Send(MsgType.Heartbeat, tr);
            }
        }

        private async Task Tick()
        {
            if (m_transport == null) return;
            var action = m_sessionState.CalcAction(m_clock.Current);
            switch (action)
            {
                case TickAction.Nothing:
                    break;

                case TickAction.TestRequest:
                {
                    m_sessionLogger?.Debug($"sending test req. state = {m_sessionState.State}");
                    await SendTestRequest();
                    break;
                }

                case TickAction.Heartbeat:
                {
                    m_sessionLogger?.Debug($"sending heartbeat. state = {m_sessionState.State}");
                    await SendHeartbeat();
                    break;
                }

                case TickAction.TerminateOnError:
                {
                    m_sessionLogger?.Warn(m_sessionState.ToString());
                    break;
                }
            }
        }
    }
}
