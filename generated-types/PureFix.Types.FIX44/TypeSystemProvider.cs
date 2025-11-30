using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;
using PureFix.Types.Config;
using PureFix.Types.Registry;

namespace PureFix.Types.FIX44
{
	/// <summary>
	/// Type system provider for registry integration.
	/// Enables dynamic loading and factory creation via ITypeRegistry.
	/// </summary>
	public class TypeSystemProvider : ITypeSystemProvider
	{
		public string GetVersion()
		{
			return "FIX.4.4";
		}
		
		public string GetRootNamespace()
		{
			return "PureFix.Types.FIX44";
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
				typeof(AllocationInstruction),
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
				typeof(MassQuoteAcknowledgement),
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
				typeof(ListStrikePrice),
				typeof(XMLnonFIX),
				typeof(RegistrationInstructions),
				typeof(RegistrationInstructionsResponse),
				typeof(OrderMassCancelRequest),
				typeof(OrderMassCancelReport),
				typeof(NewOrderCross),
				typeof(CrossOrderCancelReplaceRequest),
				typeof(CrossOrderCancelRequest),
				typeof(SecurityTypeRequest),
				typeof(SecurityTypes),
				typeof(SecurityListRequest),
				typeof(SecurityList),
				typeof(DerivativeSecurityListRequest),
				typeof(DerivativeSecurityList),
				typeof(NewOrderMultileg),
				typeof(MultilegOrderCancelReplace),
				typeof(TradeCaptureReportRequest),
				typeof(TradeCaptureReport),
				typeof(OrderMassStatusRequest),
				typeof(QuoteRequestReject),
				typeof(RFQRequest),
				typeof(QuoteStatusReport),
				typeof(QuoteResponse),
				typeof(Confirmation),
				typeof(PositionMaintenanceRequest),
				typeof(PositionMaintenanceReport),
				typeof(RequestForPositions),
				typeof(RequestForPositionsAck),
				typeof(PositionReport),
				typeof(TradeCaptureReportRequestAck),
				typeof(TradeCaptureReportAck),
				typeof(AllocationReport),
				typeof(AllocationReportAck),
				typeof(ConfirmationAck),
				typeof(SettlementInstructionRequest),
				typeof(AssignmentReport),
				typeof(CollateralRequest),
				typeof(CollateralAssignment),
				typeof(CollateralResponse),
				typeof(CollateralReport),
				typeof(CollateralInquiry),
				typeof(NetworkCounterpartySystemStatusRequest),
				typeof(NetworkCounterpartySystemStatusResponse),
				typeof(UserRequest),
				typeof(UserResponse),
				typeof(CollateralInquiryAck),
				typeof(ConfirmationRequest)
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
				"J" => typeof(AllocationInstruction),
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
				"b" => typeof(MassQuoteAcknowledgement),
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
				"n" => typeof(XMLnonFIX),
				"o" => typeof(RegistrationInstructions),
				"p" => typeof(RegistrationInstructionsResponse),
				"q" => typeof(OrderMassCancelRequest),
				"r" => typeof(OrderMassCancelReport),
				"s" => typeof(NewOrderCross),
				"t" => typeof(CrossOrderCancelReplaceRequest),
				"u" => typeof(CrossOrderCancelRequest),
				"v" => typeof(SecurityTypeRequest),
				"w" => typeof(SecurityTypes),
				"x" => typeof(SecurityListRequest),
				"y" => typeof(SecurityList),
				"z" => typeof(DerivativeSecurityListRequest),
				"AA" => typeof(DerivativeSecurityList),
				"AB" => typeof(NewOrderMultileg),
				"AC" => typeof(MultilegOrderCancelReplace),
				"AD" => typeof(TradeCaptureReportRequest),
				"AE" => typeof(TradeCaptureReport),
				"AF" => typeof(OrderMassStatusRequest),
				"AG" => typeof(QuoteRequestReject),
				"AH" => typeof(RFQRequest),
				"AI" => typeof(QuoteStatusReport),
				"AJ" => typeof(QuoteResponse),
				"AK" => typeof(Confirmation),
				"AL" => typeof(PositionMaintenanceRequest),
				"AM" => typeof(PositionMaintenanceReport),
				"AN" => typeof(RequestForPositions),
				"AO" => typeof(RequestForPositionsAck),
				"AP" => typeof(PositionReport),
				"AQ" => typeof(TradeCaptureReportRequestAck),
				"AR" => typeof(TradeCaptureReportAck),
				"AS" => typeof(AllocationReport),
				"AT" => typeof(AllocationReportAck),
				"AU" => typeof(ConfirmationAck),
				"AV" => typeof(SettlementInstructionRequest),
				"AW" => typeof(AssignmentReport),
				"AX" => typeof(CollateralRequest),
				"AY" => typeof(CollateralAssignment),
				"AZ" => typeof(CollateralResponse),
				"BA" => typeof(CollateralReport),
				"BB" => typeof(CollateralInquiry),
				"BC" => typeof(NetworkCounterpartySystemStatusRequest),
				"BD" => typeof(NetworkCounterpartySystemStatusResponse),
				"BE" => typeof(UserRequest),
				"BF" => typeof(UserResponse),
				"BG" => typeof(CollateralInquiryAck),
				"BH" => typeof(ConfirmationRequest),
				_ => null
			}
			;
		}
	}
}
