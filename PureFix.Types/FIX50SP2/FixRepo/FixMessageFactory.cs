using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public class FixMessageFactory : IFixMessageFactory
	{
		public IFixMessage? ToFixMessage(IMessageView view)
		{
			var msgType = view.GetString((int)MsgTag.MsgType);
			switch (msgType)
			{
				case "Heartbeat":
				{
					var o = new Heartbeat();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TestRequest":
				{
					var o = new TestRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ResendRequest":
				{
					var o = new ResendRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Reject":
				{
					var o = new Reject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SequenceReset":
				{
					var o = new SequenceReset();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Logout":
				{
					var o = new Logout();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "IOI":
				{
					var o = new IOI();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Advertisement":
				{
					var o = new Advertisement();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ExecutionReport":
				{
					var o = new ExecutionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderCancelReject":
				{
					var o = new OrderCancelReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Logon":
				{
					var o = new Logon();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "News":
				{
					var o = new News();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Email":
				{
					var o = new Email();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "NewOrderSingle":
				{
					var o = new NewOrderSingle();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "NewOrderList":
				{
					var o = new NewOrderList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderCancelRequest":
				{
					var o = new OrderCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderCancelReplaceRequest":
				{
					var o = new OrderCancelReplaceRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderStatusRequest":
				{
					var o = new OrderStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AllocationInstruction":
				{
					var o = new AllocationInstruction();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ListCancelRequest":
				{
					var o = new ListCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ListExecute":
				{
					var o = new ListExecute();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ListStatusRequest":
				{
					var o = new ListStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ListStatus":
				{
					var o = new ListStatus();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AllocationInstructionAck":
				{
					var o = new AllocationInstructionAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "DontKnowTrade":
				{
					var o = new DontKnowTrade();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "QuoteRequest":
				{
					var o = new QuoteRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Quote":
				{
					var o = new Quote();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SettlementInstructions":
				{
					var o = new SettlementInstructions();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDataRequest":
				{
					var o = new MarketDataRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDataSnapshotFullRefresh":
				{
					var o = new MarketDataSnapshotFullRefresh();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDataIncrementalRefresh":
				{
					var o = new MarketDataIncrementalRefresh();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDataRequestReject":
				{
					var o = new MarketDataRequestReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "QuoteCancel":
				{
					var o = new QuoteCancel();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "QuoteStatusRequest":
				{
					var o = new QuoteStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MassQuoteAcknowledgement":
				{
					var o = new MassQuoteAcknowledgement();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityDefinitionRequest":
				{
					var o = new SecurityDefinitionRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityDefinition":
				{
					var o = new SecurityDefinition();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityStatusRequest":
				{
					var o = new SecurityStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityStatus":
				{
					var o = new SecurityStatus();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradingSessionStatusRequest":
				{
					var o = new TradingSessionStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradingSessionStatus":
				{
					var o = new TradingSessionStatus();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MassQuote":
				{
					var o = new MassQuote();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BusinessMessageReject":
				{
					var o = new BusinessMessageReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BidRequest":
				{
					var o = new BidRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BidResponse":
				{
					var o = new BidResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ListStrikePrice":
				{
					var o = new ListStrikePrice();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "XMLnonFIX":
				{
					var o = new XMLnonFIX();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "RegistrationInstructions":
				{
					var o = new RegistrationInstructions();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "RegistrationInstructionsResponse":
				{
					var o = new RegistrationInstructionsResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderMassCancelRequest":
				{
					var o = new OrderMassCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderMassCancelReport":
				{
					var o = new OrderMassCancelReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "NewOrderCross":
				{
					var o = new NewOrderCross();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CrossOrderCancelReplaceRequest":
				{
					var o = new CrossOrderCancelReplaceRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CrossOrderCancelRequest":
				{
					var o = new CrossOrderCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityTypeRequest":
				{
					var o = new SecurityTypeRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityTypes":
				{
					var o = new SecurityTypes();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityListRequest":
				{
					var o = new SecurityListRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityList":
				{
					var o = new SecurityList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "DerivativeSecurityListRequest":
				{
					var o = new DerivativeSecurityListRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "DerivativeSecurityList":
				{
					var o = new DerivativeSecurityList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "NewOrderMultileg":
				{
					var o = new NewOrderMultileg();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MultilegOrderCancelReplace":
				{
					var o = new MultilegOrderCancelReplace();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradeCaptureReportRequest":
				{
					var o = new TradeCaptureReportRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradeCaptureReport":
				{
					var o = new TradeCaptureReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderMassStatusRequest":
				{
					var o = new OrderMassStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "QuoteRequestReject":
				{
					var o = new QuoteRequestReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "RFQRequest":
				{
					var o = new RFQRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "QuoteStatusReport":
				{
					var o = new QuoteStatusReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "QuoteResponse":
				{
					var o = new QuoteResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Confirmation":
				{
					var o = new Confirmation();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "PositionMaintenanceRequest":
				{
					var o = new PositionMaintenanceRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "PositionMaintenanceReport":
				{
					var o = new PositionMaintenanceReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "RequestForPositions":
				{
					var o = new RequestForPositions();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "RequestForPositionsAck":
				{
					var o = new RequestForPositionsAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "PositionReport":
				{
					var o = new PositionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradeCaptureReportRequestAck":
				{
					var o = new TradeCaptureReportRequestAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradeCaptureReportAck":
				{
					var o = new TradeCaptureReportAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AllocationReport":
				{
					var o = new AllocationReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AllocationReportAck":
				{
					var o = new AllocationReportAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ConfirmationAck":
				{
					var o = new ConfirmationAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SettlementInstructionRequest":
				{
					var o = new SettlementInstructionRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AssignmentReport":
				{
					var o = new AssignmentReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CollateralRequest":
				{
					var o = new CollateralRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CollateralAssignment":
				{
					var o = new CollateralAssignment();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CollateralResponse":
				{
					var o = new CollateralResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CollateralReport":
				{
					var o = new CollateralReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CollateralInquiry":
				{
					var o = new CollateralInquiry();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "NetworkCounterpartySystemStatusRequest":
				{
					var o = new NetworkCounterpartySystemStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "NetworkCounterpartySystemStatusResponse":
				{
					var o = new NetworkCounterpartySystemStatusResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "UserRequest":
				{
					var o = new UserRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "UserResponse":
				{
					var o = new UserResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "CollateralInquiryAck":
				{
					var o = new CollateralInquiryAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ConfirmationRequest":
				{
					var o = new ConfirmationRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ContraryIntentionReport":
				{
					var o = new ContraryIntentionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityDefinitionUpdateReport":
				{
					var o = new SecurityDefinitionUpdateReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SecurityListUpdateReport":
				{
					var o = new SecurityListUpdateReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AdjustedPositionReport":
				{
					var o = new AdjustedPositionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AllocationInstructionAlert":
				{
					var o = new AllocationInstructionAlert();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ExecutionAcknowledgement":
				{
					var o = new ExecutionAcknowledgement();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradingSessionList":
				{
					var o = new TradingSessionList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradingSessionListRequest":
				{
					var o = new TradingSessionListRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "SettlementObligationReport":
				{
					var o = new SettlementObligationReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "DerivativeSecurityListUpdateReport":
				{
					var o = new DerivativeSecurityListUpdateReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "TradingSessionListUpdateReport":
				{
					var o = new TradingSessionListUpdateReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDefinitionRequest":
				{
					var o = new MarketDefinitionRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDefinition":
				{
					var o = new MarketDefinition();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "MarketDefinitionUpdateReport":
				{
					var o = new MarketDefinitionUpdateReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "UserNotification":
				{
					var o = new UserNotification();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderMassActionReport":
				{
					var o = new OrderMassActionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "OrderMassActionRequest":
				{
					var o = new OrderMassActionRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ApplicationMessageRequest":
				{
					var o = new ApplicationMessageRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ApplicationMessageRequestAck":
				{
					var o = new ApplicationMessageRequestAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "ApplicationMessageReport":
				{
					var o = new ApplicationMessageReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "StreamAssignmentRequest":
				{
					var o = new StreamAssignmentRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "StreamAssignmentReport":
				{
					var o = new StreamAssignmentReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "StreamAssignmentReportACK":
				{
					var o = new StreamAssignmentReportACK();
					((IFixParser)o).Parse(view);
					return o;
				}
			}
			return null;
		}
	}
}
