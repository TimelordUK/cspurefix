using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UnderlyingInstrument
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (UnderlyingSymbol != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(311);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSymbol);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 311);
			}
			if (UnderlyingSymbolSfx != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(312);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSymbolSfx);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 312);
			}
			if (UnderlyingSecurityID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(309);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 309);
			}
			if (UnderlyingSecurityIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(305);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 305);
			}
			((IFixEncoder)UndSecAltIDGrp!)?.Encode(storage, tags, delimiter);
			if (UnderlyingProduct != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(462);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)UnderlyingProduct);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 462);
			}
			if (UnderlyingCFICode != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(463);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingCFICode);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 463);
			}
			if (UnderlyingSecurityType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(310);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 310);
			}
			if (UnderlyingSecuritySubType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(763);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecuritySubType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 763);
			}
			if (UnderlyingMaturityMonthYear != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(313);
				storage.WriteChar((byte)'=');
				storage.WriteMonthYear((MonthYear)UnderlyingMaturityMonthYear);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 313);
			}
			if (UnderlyingMaturityDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(542);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)UnderlyingMaturityDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 542);
			}
			if (UnderlyingPutOrCall != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(315);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)UnderlyingPutOrCall);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 315);
			}
			if (UnderlyingCouponPaymentDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(241);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)UnderlyingCouponPaymentDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 241);
			}
			if (UnderlyingIssueDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(242);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)UnderlyingIssueDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 242);
			}
			if (UnderlyingRepoCollateralSecurityType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(243);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingRepoCollateralSecurityType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 243);
			}
			if (UnderlyingRepurchaseTerm != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(244);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)UnderlyingRepurchaseTerm);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 244);
			}
			if (UnderlyingRepurchaseRate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(245);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingRepurchaseRate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 245);
			}
			if (UnderlyingFactor != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(246);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingFactor);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 246);
			}
			if (UnderlyingCreditRating != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(256);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingCreditRating);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 256);
			}
			if (UnderlyingInstrRegistry != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(595);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingInstrRegistry);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 595);
			}
			if (UnderlyingCountryOfIssue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(592);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingCountryOfIssue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 592);
			}
			if (UnderlyingStateOrProvinceOfIssue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(593);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingStateOrProvinceOfIssue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 593);
			}
			if (UnderlyingLocaleOfIssue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(594);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingLocaleOfIssue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 594);
			}
			if (UnderlyingRedemptionDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(247);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)UnderlyingRedemptionDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 247);
			}
			if (UnderlyingStrikePrice != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(316);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingStrikePrice);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 316);
			}
			if (UnderlyingStrikeCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(941);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingStrikeCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 941);
			}
			if (UnderlyingOptAttribute != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(317);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingOptAttribute);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 317);
			}
			if (UnderlyingContractMultiplier != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(436);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingContractMultiplier);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 436);
			}
			if (UnderlyingCouponRate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(435);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingCouponRate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 435);
			}
			if (UnderlyingSecurityExchange != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(308);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityExchange);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 308);
			}
			if (UnderlyingIssuer != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(306);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingIssuer);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 306);
			}
			if (EncodedUnderlyingIssuerLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(362);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)EncodedUnderlyingIssuerLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 362);
			}
			if (EncodedUnderlyingIssuer != null)
			{
				if (EncodedUnderlyingIssuerLen == null)
				{
					{
						var at = storage.Pos;
						storage.WriteWholeNumber(362);
						storage.WriteChar((byte)'=');
						storage.WriteWholeNumber((int)EncodedUnderlyingIssuer.Length);
						storage.WriteChar(delimiter);
						tags.Store(at, storage.Pos - at, 362);
					}
				}
				{
					var at = storage.Pos;
					storage.WriteWholeNumber(363);
					storage.WriteChar((byte)'=');
					storage.WriteBuffer((byte[])EncodedUnderlyingIssuer);
					storage.WriteChar(delimiter);
					tags.Store(at, storage.Pos - at, 363);
				}
			}
			if (UnderlyingSecurityDesc != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(307);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityDesc);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 307);
			}
			if (EncodedUnderlyingSecurityDescLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(364);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)EncodedUnderlyingSecurityDescLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 364);
			}
			if (EncodedUnderlyingSecurityDesc != null)
			{
				if (EncodedUnderlyingSecurityDescLen == null)
				{
					{
						var at = storage.Pos;
						storage.WriteWholeNumber(364);
						storage.WriteChar((byte)'=');
						storage.WriteWholeNumber((int)EncodedUnderlyingSecurityDesc.Length);
						storage.WriteChar(delimiter);
						tags.Store(at, storage.Pos - at, 364);
					}
				}
				{
					var at = storage.Pos;
					storage.WriteWholeNumber(365);
					storage.WriteChar((byte)'=');
					storage.WriteBuffer((byte[])EncodedUnderlyingSecurityDesc);
					storage.WriteChar(delimiter);
					tags.Store(at, storage.Pos - at, 365);
				}
			}
			if (UnderlyingCPProgram != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(877);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingCPProgram);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 877);
			}
			if (UnderlyingCPRegType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(878);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingCPRegType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 878);
			}
			if (UnderlyingCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(318);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 318);
			}
			if (UnderlyingQty != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(879);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingQty);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 879);
			}
			if (UnderlyingPx != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(810);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingPx);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 810);
			}
			if (UnderlyingDirtyPrice != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(882);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingDirtyPrice);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 882);
			}
			if (UnderlyingEndPrice != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(883);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingEndPrice);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 883);
			}
			if (UnderlyingStartValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(884);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingStartValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 884);
			}
			if (UnderlyingCurrentValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(885);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingCurrentValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 885);
			}
			if (UnderlyingEndValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(886);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)UnderlyingEndValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 886);
			}
			((IFixEncoder)UnderlyingStipulations!)?.Encode(storage, tags, delimiter);
		}
	}
}
