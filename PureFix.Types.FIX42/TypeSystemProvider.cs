using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;
using PureFix.Types.Config;
using PureFix.Types.Registry;

namespace PureFix.Types.FIX42
{
	/// <summary>
	/// Type system provider for registry integration.
	/// Enables dynamic loading and factory creation via ITypeRegistry.
	/// </summary>
	public class TypeSystemProvider : ITypeSystemProvider
	{
		public string GetVersion()
		{
			return "FIX.4.2";
		}
		
		public string GetRootNamespace()
		{
			return "PureFix.Types.FIX42";
		}
		
		public IFixMessageFactory CreateMessageFactory()
		{
			return new FixMessageFactory();
		}
		
		public ISessionMessageFactory CreateSessionMessageFactory(ISessionDescription sessionDescription)
		{
			return new SessionMessageFactory(sessionDescription);
		}
		
		public IEnumerable<Type> GetMessageTypes()
		{
			return new Type[]
			{
				typeof(Heartbeat),
				typeof(TestRequest),
				typeof(ResendRequest),
				typeof(Reject),
				typeof(SequenceReset),
				typeof(Logout),
				typeof(IOI),
				typeof(Advertisement),
				typeof(ExecutionReport),
				typeof(OrderCancelReject),
				typeof(Logon),
				typeof(News),
				typeof(Email),
				typeof(NewOrderSingle),
				typeof(NewOrderList),
				typeof(OrderCancelRequest),
				typeof(OrderCancelReplaceRequest),
				typeof(OrderStatusRequest),
				typeof(Allocation),
				typeof(ListCancelRequest),
				typeof(ListExecute),
				typeof(ListStatusRequest),
				typeof(ListStatus),
				typeof(AllocationInstructionAck),
				typeof(DontKnowTrade),
				typeof(QuoteRequest),
				typeof(Quote),
				typeof(SettlementInstructions),
				typeof(MarketDataRequest),
				typeof(MarketDataSnapshotFullRefresh),
				typeof(MarketDataIncrementalRefresh),
				typeof(MarketDataRequestReject),
				typeof(QuoteCancel),
				typeof(QuoteStatusRequest),
				typeof(QuoteAcknowledgement),
				typeof(SecurityDefinitionRequest),
				typeof(SecurityDefinition),
				typeof(SecurityStatusRequest),
				typeof(SecurityStatus),
				typeof(TradingSessionStatusRequest),
				typeof(TradingSessionStatus),
				typeof(MassQuote),
				typeof(BusinessMessageReject),
				typeof(BidRequest),
				typeof(BidResponse),
				typeof(ListStrikePrice)
			}
			;
		}
		
		public Type? GetMessageTypeByMsgType(string msgType)
		{
			return msgType switch
			{
				"0" => typeof(Heartbeat),
				"1" => typeof(TestRequest),
				"2" => typeof(ResendRequest),
				"3" => typeof(Reject),
				"4" => typeof(SequenceReset),
				"5" => typeof(Logout),
				"6" => typeof(IOI),
				"7" => typeof(Advertisement),
				"8" => typeof(ExecutionReport),
				"9" => typeof(OrderCancelReject),
				"A" => typeof(Logon),
				"B" => typeof(News),
				"C" => typeof(Email),
				"D" => typeof(NewOrderSingle),
				"E" => typeof(NewOrderList),
				"F" => typeof(OrderCancelRequest),
				"G" => typeof(OrderCancelReplaceRequest),
				"H" => typeof(OrderStatusRequest),
				"J" => typeof(Allocation),
				"K" => typeof(ListCancelRequest),
				"L" => typeof(ListExecute),
				"M" => typeof(ListStatusRequest),
				"N" => typeof(ListStatus),
				"P" => typeof(AllocationInstructionAck),
				"Q" => typeof(DontKnowTrade),
				"R" => typeof(QuoteRequest),
				"S" => typeof(Quote),
				"T" => typeof(SettlementInstructions),
				"V" => typeof(MarketDataRequest),
				"W" => typeof(MarketDataSnapshotFullRefresh),
				"X" => typeof(MarketDataIncrementalRefresh),
				"Y" => typeof(MarketDataRequestReject),
				"Z" => typeof(QuoteCancel),
				"a" => typeof(QuoteStatusRequest),
				"b" => typeof(QuoteAcknowledgement),
				"c" => typeof(SecurityDefinitionRequest),
				"d" => typeof(SecurityDefinition),
				"e" => typeof(SecurityStatusRequest),
				"f" => typeof(SecurityStatus),
				"g" => typeof(TradingSessionStatusRequest),
				"h" => typeof(TradingSessionStatus),
				"i" => typeof(MassQuote),
				"j" => typeof(BusinessMessageReject),
				"k" => typeof(BidRequest),
				"l" => typeof(BidResponse),
				"m" => typeof(ListStrikePrice),
				_ => null
			}
			;
		}
	}
}
