using System.Diagnostics.CodeAnalysis;

namespace PureFix.Types.FIX43.QuickFix.Types
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
				"P" => self.MakeAndParse<AllocationAck>(view),
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
				"b" => self.MakeAndParse<MassQuoteAcknowledgement>(view),
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
				"n" => self.MakeAndParse<XMLnonFIX>(view),
				"o" => self.MakeAndParse<RegistrationInstructions>(view),
				"p" => self.MakeAndParse<RegistrationInstructionsResponse>(view),
				"q" => self.MakeAndParse<OrderMassCancelRequest>(view),
				"r" => self.MakeAndParse<OrderMassCancelReport>(view),
				"s" => self.MakeAndParse<NewOrderCross>(view),
				"t" => self.MakeAndParse<CrossOrderCancelReplaceRequest>(view),
				"u" => self.MakeAndParse<CrossOrderCancelRequest>(view),
				"v" => self.MakeAndParse<SecurityTypeRequest>(view),
				"w" => self.MakeAndParse<SecurityTypes>(view),
				"x" => self.MakeAndParse<SecurityListRequest>(view),
				"y" => self.MakeAndParse<SecurityList>(view),
				"z" => self.MakeAndParse<DerivativeSecurityListRequest>(view),
				"AA" => self.MakeAndParse<DerivativeSecurityList>(view),
				"AB" => self.MakeAndParse<NewOrderMultileg>(view),
				"AC" => self.MakeAndParse<MultilegOrderCancelReplaceRequest>(view),
				"AD" => self.MakeAndParse<TradeCaptureReportRequest>(view),
				"AE" => self.MakeAndParse<TradeCaptureReport>(view),
				"AF" => self.MakeAndParse<OrderMassStatusRequest>(view),
				"AG" => self.MakeAndParse<QuoteRequestReject>(view),
				"AH" => self.MakeAndParse<RFQRequest>(view),
				"AI" => self.MakeAndParse<QuoteStatusReport>(view),
				_ => null
			};
			return message is not null;
		}
	}
}
