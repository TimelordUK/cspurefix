using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class Instrument
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (Symbol != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(55);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Symbol);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 55);
			}
			if (SymbolSfx != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(65);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SymbolSfx);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 65);
			}
			if (SecurityID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(48);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 48);
			}
			if (SecurityIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(22);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 22);
			}
			((IFixEncoder)SecAltIDGrp!)?.Encode(storage, tags, delimiter);
			if (Product != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(460);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)Product);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 460);
			}
			if (CFICode != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(461);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CFICode);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 461);
			}
			if (SecurityType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(167);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 167);
			}
			if (SecuritySubType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(762);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecuritySubType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 762);
			}
			if (MaturityMonthYear != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(200);
				storage.WriteChar((byte)'=');
				storage.WriteMonthYear((MonthYear)MaturityMonthYear);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 200);
			}
			if (MaturityDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(541);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)MaturityDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 541);
			}
			if (PutOrCall != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(201);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PutOrCall);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 201);
			}
			if (CouponPaymentDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(224);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)CouponPaymentDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 224);
			}
			if (IssueDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(225);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)IssueDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 225);
			}
			if (RepoCollateralSecurityType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(239);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)RepoCollateralSecurityType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 239);
			}
			if (RepurchaseTerm != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(226);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)RepurchaseTerm);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 226);
			}
			if (RepurchaseRate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(227);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)RepurchaseRate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 227);
			}
			if (Factor != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(228);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)Factor);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 228);
			}
			if (CreditRating != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(255);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CreditRating);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 255);
			}
			if (InstrRegistry != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(543);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)InstrRegistry);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 543);
			}
			if (CountryOfIssue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(470);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CountryOfIssue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 470);
			}
			if (StateOrProvinceOfIssue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(471);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)StateOrProvinceOfIssue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 471);
			}
			if (LocaleOfIssue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(472);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)LocaleOfIssue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 472);
			}
			if (RedemptionDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(240);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)RedemptionDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 240);
			}
			if (StrikePrice != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(202);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)StrikePrice);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 202);
			}
			if (StrikeCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(947);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)StrikeCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 947);
			}
			if (OptAttribute != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(206);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OptAttribute);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 206);
			}
			if (ContractMultiplier != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(231);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)ContractMultiplier);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 231);
			}
			if (CouponRate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(223);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)CouponRate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 223);
			}
			if (SecurityExchange != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(207);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityExchange);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 207);
			}
			if (Issuer != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(106);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Issuer);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 106);
			}
			if (EncodedIssuerLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(348);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)EncodedIssuerLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 348);
			}
			if (EncodedIssuer != null)
			{
				if (EncodedIssuerLen == null)
				{
					{
						var at = storage.Pos;
						storage.WriteWholeNumber(348);
						storage.WriteChar((byte)'=');
						storage.WriteWholeNumber((int)EncodedIssuer.Length);
						storage.WriteChar(delimiter);
						tags.Store(at, storage.Pos - at, 348);
					}
				}
				{
					var at = storage.Pos;
					storage.WriteWholeNumber(349);
					storage.WriteChar((byte)'=');
					storage.WriteBuffer((byte[])EncodedIssuer);
					storage.WriteChar(delimiter);
					tags.Store(at, storage.Pos - at, 349);
				}
			}
			if (SecurityDesc != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(107);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityDesc);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 107);
			}
			if (EncodedSecurityDescLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(350);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)EncodedSecurityDescLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 350);
			}
			if (EncodedSecurityDesc != null)
			{
				if (EncodedSecurityDescLen == null)
				{
					{
						var at = storage.Pos;
						storage.WriteWholeNumber(350);
						storage.WriteChar((byte)'=');
						storage.WriteWholeNumber((int)EncodedSecurityDesc.Length);
						storage.WriteChar(delimiter);
						tags.Store(at, storage.Pos - at, 350);
					}
				}
				{
					var at = storage.Pos;
					storage.WriteWholeNumber(351);
					storage.WriteChar((byte)'=');
					storage.WriteBuffer((byte[])EncodedSecurityDesc);
					storage.WriteChar(delimiter);
					tags.Store(at, storage.Pos - at, 351);
				}
			}
			if (Pool != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(691);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)Pool);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 691);
			}
			if (ContractSettlMonth != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(667);
				storage.WriteChar((byte)'=');
				storage.WriteMonthYear((MonthYear)ContractSettlMonth);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 667);
			}
			if (CPProgram != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(875);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)CPProgram);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 875);
			}
			if (CPRegType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(876);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CPRegType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 876);
			}
			((IFixEncoder)EvntGrp!)?.Encode(storage, tags, delimiter);
			if (DatedDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(873);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)DatedDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 873);
			}
			if (InterestAccrualDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(874);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)InterestAccrualDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 874);
			}
		}
	}
}
