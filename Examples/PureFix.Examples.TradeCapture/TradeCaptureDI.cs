using Arrow.Threading.Tasks;
using PureFix.Examples.Shared;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX50SP2;

namespace PureFix.Examples.TradeCapture;

internal class TradeCaptureDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config)
    : AppHost<TradeCaptureSessionFactory, FixMessageFactory>(q, factory, clock, config)
{
}
