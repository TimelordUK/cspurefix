using System.Diagnostics.CodeAnalysis;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public class FixMessageFactory : IFixMessageFactory
	{
		public bool TryParse(IMessageView view, [NotNullWhen(true)] out IFixMessage? message)
		{
			IFixMessageFactory self = this;
			var messageType = view.GetString((int)MsgTag.MsgType);
			message = messageType switch
			{
				"0" => self.MakeAndParse<Heartbeat>(view),
				"1" => self.MakeAndParse<TestRequest>(view),
				"2" => self.MakeAndParse<ResendRequest>(view),
				"3" => self.MakeAndParse<Reject>(view),
				"4" => self.MakeAndParse<SequenceReset>(view),
				"5" => self.MakeAndParse<Logout>(view),
				"6" => self.MakeAndParse<IOI>(view),
				"7" => self.MakeAndParse<Advertisement>(view),
				"8" => self.MakeAndParse<ExecutionReport>(view),
				"9" => self.MakeAndParse<OrderCancelReject>(view),
				"A" => self.MakeAndParse<Logon>(view),
				"B" => self.MakeAndParse<News>(view),
				"C" => self.MakeAndParse<Email>(view),
				"D" => self.MakeAndParse<NewOrderSingle>(view),
				"E" => self.MakeAndParse<NewOrderList>(view),
				"F" => self.MakeAndParse<OrderCancelRequest>(view),
				"G" => self.MakeAndParse<OrderCancelReplaceRequest>(view),
				"H" => self.MakeAndParse<OrderStatusRequest>(view),
				"J" => self.MakeAndParse<Allocation>(view),
				"K" => self.MakeAndParse<ListCancelRequest>(view),
				"L" => self.MakeAndParse<ListExecute>(view),
				"M" => self.MakeAndParse<ListStatusRequest>(view),
				"N" => self.MakeAndParse<ListStatus>(view),
				"P" => self.MakeAndParse<AllocationInstructionAck>(view),
				"Q" => self.MakeAndParse<DontKnowTrade>(view),
				"R" => self.MakeAndParse<QuoteRequest>(view),
				"S" => self.MakeAndParse<Quote>(view),
				"T" => self.MakeAndParse<SettlementInstructions>(view),
				"V" => self.MakeAndParse<MarketDataRequest>(view),
				"W" => self.MakeAndParse<MarketDataSnapshotFullRefresh>(view),
				"X" => self.MakeAndParse<MarketDataIncrementalRefresh>(view),
				"Y" => self.MakeAndParse<MarketDataRequestReject>(view),
				"Z" => self.MakeAndParse<QuoteCancel>(view),
				"a" => self.MakeAndParse<QuoteStatusRequest>(view),
				"b" => self.MakeAndParse<QuoteAcknowledgement>(view),
				"c" => self.MakeAndParse<SecurityDefinitionRequest>(view),
				"d" => self.MakeAndParse<SecurityDefinition>(view),
				"e" => self.MakeAndParse<SecurityStatusRequest>(view),
				"f" => self.MakeAndParse<SecurityStatus>(view),
				"g" => self.MakeAndParse<TradingSessionStatusRequest>(view),
				"h" => self.MakeAndParse<TradingSessionStatus>(view),
				"i" => self.MakeAndParse<MassQuote>(view),
				"j" => self.MakeAndParse<BusinessMessageReject>(view),
				"k" => self.MakeAndParse<BidRequest>(view),
				"l" => self.MakeAndParse<BidResponse>(view),
				"m" => self.MakeAndParse<ListStrikePrice>(view),
				_ => null
			};
			return message is not null;
		}
	}
}
