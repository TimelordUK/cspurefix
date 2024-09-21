using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{

    public class FixMessageFactory : IFixMessageFactory
	{
		public IFixMessage? ToFixMessage(IMessageView view)
		{
			var msgType = view.GetString((int)MsgTag.MsgType);
			switch (msgType)
			{
				case "0":
				{
					var o = new Heartbeat();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "1":
				{
					var o = new TestRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "2":
				{
					var o = new ResendRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "3":
				{
					var o = new Reject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "4":
				{
					var o = new SequenceReset();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "5":
				{
					var o = new Logout();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "6":
				{
					var o = new IOI();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "7":
				{
					var o = new Advertisement();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "8":
				{
					var o = new ExecutionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "9":
				{
					var o = new OrderCancelReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "A":
				{
					var o = new Logon();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "B":
				{
					var o = new News();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "C":
				{
					var o = new Email();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "D":
				{
					var o = new NewOrderSingle();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "E":
				{
					var o = new NewOrderList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "F":
				{
					var o = new OrderCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "G":
				{
					var o = new OrderCancelReplaceRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "H":
				{
					var o = new OrderStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "J":
				{
					var o = new AllocationInstruction();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "K":
				{
					var o = new ListCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "L":
				{
					var o = new ListExecute();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "M":
				{
					var o = new ListStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "N":
				{
					var o = new ListStatus();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "P":
				{
					var o = new AllocationInstructionAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Q":
				{
					var o = new DontKnowTrade();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "R":
				{
					var o = new QuoteRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "S":
				{
					var o = new Quote();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "T":
				{
					var o = new SettlementInstructions();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "V":
				{
					var o = new MarketDataRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "W":
				{
					var o = new MarketDataSnapshotFullRefresh();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "X":
				{
					var o = new MarketDataIncrementalRefresh();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Y":
				{
					var o = new MarketDataRequestReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "Z":
				{
					var o = new QuoteCancel();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "a":
				{
					var o = new QuoteStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "b":
				{
					var o = new MassQuoteAcknowledgement();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "c":
				{
					var o = new SecurityDefinitionRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "d":
				{
					var o = new SecurityDefinition();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "e":
				{
					var o = new SecurityStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "f":
				{
					var o = new SecurityStatus();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "g":
				{
					var o = new TradingSessionStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "h":
				{
					var o = new TradingSessionStatus();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "i":
				{
					var o = new MassQuote();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "j":
				{
					var o = new BusinessMessageReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "k":
				{
					var o = new BidRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "l":
				{
					var o = new BidResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "m":
				{
					var o = new ListStrikePrice();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "n":
				{
					var o = new XMLnonFIX();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "o":
				{
					var o = new RegistrationInstructions();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "p":
				{
					var o = new RegistrationInstructionsResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "q":
				{
					var o = new OrderMassCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "r":
				{
					var o = new OrderMassCancelReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "s":
				{
					var o = new NewOrderCross();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "t":
				{
					var o = new CrossOrderCancelReplaceRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "u":
				{
					var o = new CrossOrderCancelRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "v":
				{
					var o = new SecurityTypeRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "w":
				{
					var o = new SecurityTypes();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "x":
				{
					var o = new SecurityListRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "y":
				{
					var o = new SecurityList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "z":
				{
					var o = new DerivativeSecurityListRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AA":
				{
					var o = new DerivativeSecurityList();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AB":
				{
					var o = new NewOrderMultileg();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AC":
				{
					var o = new MultilegOrderCancelReplace();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AD":
				{
					var o = new TradeCaptureReportRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AE":
				{
					var o = new TradeCaptureReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AF":
				{
					var o = new OrderMassStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AG":
				{
					var o = new QuoteRequestReject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AH":
				{
					var o = new RFQRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AI":
				{
					var o = new QuoteStatusReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AJ":
				{
					var o = new QuoteResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AK":
				{
					var o = new Confirmation();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AL":
				{
					var o = new PositionMaintenanceRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AM":
				{
					var o = new PositionMaintenanceReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AN":
				{
					var o = new RequestForPositions();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AO":
				{
					var o = new RequestForPositionsAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AP":
				{
					var o = new PositionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AQ":
				{
					var o = new TradeCaptureReportRequestAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AR":
				{
					var o = new TradeCaptureReportAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AS":
				{
					var o = new AllocationReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AT":
				{
					var o = new AllocationReportAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AU":
				{
					var o = new ConfirmationAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AV":
				{
					var o = new SettlementInstructionRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AW":
				{
					var o = new AssignmentReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AX":
				{
					var o = new CollateralRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AY":
				{
					var o = new CollateralAssignment();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AZ":
				{
					var o = new CollateralResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BA":
				{
					var o = new CollateralReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BB":
				{
					var o = new CollateralInquiry();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BC":
				{
					var o = new NetworkCounterpartySystemStatusRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BD":
				{
					var o = new NetworkCounterpartySystemStatusResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BE":
				{
					var o = new UserRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BF":
				{
					var o = new UserResponse();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BG":
				{
					var o = new CollateralInquiryAck();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "BH":
				{
					var o = new ConfirmationRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
			}
			return null;
		}
	}
}
