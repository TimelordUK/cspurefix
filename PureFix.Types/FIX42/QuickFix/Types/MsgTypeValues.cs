namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class MsgTypeValues
	{
		public const string Heartbeat = "0";
		public const string TestRequest = "1";
		public const string ResendRequest = "2";
		public const string Reject = "3";
		public const string SequenceReset = "4";
		public const string Logout = "5";
		public const string Ioi = "6";
		public const string Advertisement = "7";
		public const string ExecutionReport = "8";
		public const string OrderCancelReject = "9";
		public const string Logon = "A";
		public const string News = "B";
		public const string Email = "C";
		public const string NewOrderSingle = "D";
		public const string NewOrderList = "E";
		public const string OrderCancelRequest = "F";
		public const string OrderCancelReplaceRequest = "G";
		public const string OrderStatusRequest = "H";
		public const string AllocationInstruction = "J";
		public const string ListCancelRequest = "K";
		public const string ListExecute = "L";
		public const string ListStatusRequest = "M";
		public const string ListStatus = "N";
		public const string AllocationInstructionAck = "P";
		public const string DontKnowTrade = "Q";
		public const string QuoteRequest = "R";
		public const string Quote = "S";
		public const string SettlementInstructions = "T";
		public const string MarketDataRequest = "V";
		public const string MarketDataSnapshotFullRefresh = "W";
		public const string MarketDataIncrementalRefresh = "X";
		public const string MarketDataRequestReject = "Y";
		public const string QuoteCancel = "Z";
		public const string QuoteStatusRequest = "a";
		public const string MassQuoteAcknowledgement = "b";
		public const string SecurityDefinitionRequest = "c";
		public const string SecurityDefinition = "d";
		public const string SecurityStatusRequest = "e";
		public const string SecurityStatus = "f";
		public const string TradingSessionStatusRequest = "g";
		public const string TradingSessionStatus = "h";
		public const string MassQuote = "i";
		public const string BusinessMessageReject = "j";
		public const string BidRequest = "k";
		public const string BidResponse = "l";
		public const string ListStrikePrice = "m";
	}
}
