using PureFix.Examples.Shared;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX50SP2;

namespace PureFix.Examples.TradeCapture;

internal class TradeCaptureDI(ILogFactory factory, IFixClock clock, IFixConfig config)
    : AppHost<TradeCaptureSessionFactory, FixMessageFactory, Fix50SP2SessionMessageFactory>(factory, clock, config)
{
}
