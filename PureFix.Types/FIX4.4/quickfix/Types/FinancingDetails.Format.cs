using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class FinancingDetails
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (AgreementDesc != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(913);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)AgreementDesc);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 913);
			}
			if (AgreementID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(914);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)AgreementID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 914);
			}
			if (AgreementDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(915);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)AgreementDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 915);
			}
			if (AgreementCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(918);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)AgreementCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 918);
			}
			if (TerminationType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(788);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)TerminationType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 788);
			}
			if (StartDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(916);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)StartDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 916);
			}
			if (EndDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(917);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)EndDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 917);
			}
			if (DeliveryType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(919);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)DeliveryType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 919);
			}
			if (MarginRatio != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(898);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)MarginRatio);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 898);
			}
		}
	}
}
