using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed partial class NewOrderSingle
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			((IFixEncoder)StandardHeader!)?.Encode(storage, tags, delimiter);
			if (ClOrdID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(11);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ClOrdID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 11);
			}
			if (SecondaryClOrdID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(526);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecondaryClOrdID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 526);
			}
			if (ClOrdLinkID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(583);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ClOrdLinkID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 583);
			}
			((IFixEncoder)Parties!)?.Encode(storage, tags, delimiter);
			if (TradeOriginationDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(229);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)TradeOriginationDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 229);
			}
			if (TradeDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(75);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)TradeDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 75);
			}
			if (Account != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(1);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Account);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 1);
			}
			if (AcctIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(660);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)AcctIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 660);
			}
			if (AccountType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(581);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)AccountType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 581);
			}
			if (DayBookingInst != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(589);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)DayBookingInst);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 589);
			}
			if (BookingUnit != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(590);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BookingUnit);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 590);
			}
			if (PreallocMethod != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(591);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)PreallocMethod);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 591);
			}
			if (AllocID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(70);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)AllocID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 70);
			}
			((IFixEncoder)PreAllocGrp!)?.Encode(storage, tags, delimiter);
			if (SettlType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(63);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SettlType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 63);
			}
			if (SettlDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(64);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)SettlDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 64);
			}
			if (CashMargin != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(544);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CashMargin);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 544);
			}
			if (ClearingFeeIndicator != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(635);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ClearingFeeIndicator);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 635);
			}
			if (HandlInst != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(21);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)HandlInst);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 21);
			}
			if (ExecInst != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(18);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ExecInst);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 18);
			}
			if (MinQty != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(110);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)MinQty);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 110);
			}
			if (MaxFloor != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(111);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)MaxFloor);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 111);
			}
			if (ExDestination != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(100);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ExDestination);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 100);
			}
			((IFixEncoder)TrdgSesGrp!)?.Encode(storage, tags, delimiter);
			if (ProcessCode != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(81);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ProcessCode);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 81);
			}
			((IFixEncoder)Instrument!)?.Encode(storage, tags, delimiter);
			((IFixEncoder)FinancingDetails!)?.Encode(storage, tags, delimiter);
			((IFixEncoder)UndInstrmtGrp!)?.Encode(storage, tags, delimiter);
			if (PrevClosePx != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(140);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)PrevClosePx);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 140);
			}
			if (Side != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(54);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Side);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 54);
			}
			if (LocateReqd != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(114);
				storage.WriteChar((byte)'=');
				storage.WriteBoolean((bool)LocateReqd);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 114);
			}
			if (TransactTime != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(60);
				storage.WriteChar((byte)'=');
				storage.WriteUtcTimeStamp((DateTime)TransactTime);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 60);
			}
			((IFixEncoder)Stipulations!)?.Encode(storage, tags, delimiter);
			if (QtyType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(854);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)QtyType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 854);
			}
			((IFixEncoder)OrderQtyData!)?.Encode(storage, tags, delimiter);
			if (OrdType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(40);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OrdType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 40);
			}
			if (PriceType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(423);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PriceType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 423);
			}
			if (Price != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(44);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)Price);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 44);
			}
			if (StopPx != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(99);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)StopPx);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 99);
			}
			((IFixEncoder)SpreadOrBenchmarkCurveData!)?.Encode(storage, tags, delimiter);
			((IFixEncoder)YieldData!)?.Encode(storage, tags, delimiter);
			if (Currency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(15);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Currency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 15);
			}
			if (ComplianceID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(376);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)ComplianceID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 376);
			}
			if (SolicitedFlag != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(377);
				storage.WriteChar((byte)'=');
				storage.WriteBoolean((bool)SolicitedFlag);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 377);
			}
			if (IOIID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(23);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)IOIID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 23);
			}
			if (QuoteID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(117);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)QuoteID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 117);
			}
			if (TimeInForce != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(59);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TimeInForce);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 59);
			}
			if (EffectiveTime != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(168);
				storage.WriteChar((byte)'=');
				storage.WriteUtcTimeStamp((DateTime)EffectiveTime);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 168);
			}
			if (ExpireDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(432);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)ExpireDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 432);
			}
			if (ExpireTime != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(126);
				storage.WriteChar((byte)'=');
				storage.WriteUtcTimeStamp((DateTime)ExpireTime);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 126);
			}
			if (GTBookingInst != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(427);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)GTBookingInst);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 427);
			}
			((IFixEncoder)CommissionData!)?.Encode(storage, tags, delimiter);
			if (OrderCapacity != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(528);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OrderCapacity);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 528);
			}
			if (OrderRestrictions != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(529);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OrderRestrictions);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 529);
			}
			if (CustOrderCapacity != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(582);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)CustOrderCapacity);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 582);
			}
			if (ForexReq != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(121);
				storage.WriteChar((byte)'=');
				storage.WriteBoolean((bool)ForexReq);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 121);
			}
			if (SettlCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(120);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SettlCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 120);
			}
			if (BookingType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(775);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)BookingType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 775);
			}
			if (Text != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(58);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Text);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 58);
			}
			if (EncodedTextLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(354);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)EncodedTextLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 354);
			}
			if (EncodedText != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(355);
				storage.WriteChar((byte)'=');
				storage.WriteBuffer((byte[])EncodedText);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 355);
			}
			if (SettlDate2 != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(193);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)SettlDate2);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 193);
			}
			if (OrderQty2 != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(192);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)OrderQty2);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 192);
			}
			if (Price2 != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(640);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)Price2);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 640);
			}
			if (PositionEffect != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(77);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)PositionEffect);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 77);
			}
			if (CoveredOrUncovered != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(203);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)CoveredOrUncovered);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 203);
			}
			if (MaxShow != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(210);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)MaxShow);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 210);
			}
			((IFixEncoder)PegInstructions!)?.Encode(storage, tags, delimiter);
			((IFixEncoder)DiscretionInstructions!)?.Encode(storage, tags, delimiter);
			if (TargetStrategy != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(847);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)TargetStrategy);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 847);
			}
			if (TargetStrategyParameters != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(848);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TargetStrategyParameters);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 848);
			}
			if (ParticipationRate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(849);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)ParticipationRate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 849);
			}
			if (CancellationRights != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(480);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CancellationRights);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 480);
			}
			if (MoneyLaunderingStatus != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(481);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)MoneyLaunderingStatus);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 481);
			}
			if (RegistID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(513);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)RegistID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 513);
			}
			if (Designation != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(494);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Designation);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 494);
			}
			((IFixEncoder)StandardTrailer!)?.Encode(storage, tags, delimiter);
		}
	}
}
