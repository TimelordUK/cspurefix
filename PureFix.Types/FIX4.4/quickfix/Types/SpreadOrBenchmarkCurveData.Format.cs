using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SpreadOrBenchmarkCurveData
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (Spread != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(218);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)Spread);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 218);
			}
			if (BenchmarkCurveCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(220);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BenchmarkCurveCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 220);
			}
			if (BenchmarkCurveName != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(221);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BenchmarkCurveName);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 221);
			}
			if (BenchmarkCurvePoint != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(222);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BenchmarkCurvePoint);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 222);
			}
			if (BenchmarkPrice != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(662);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)BenchmarkPrice);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 662);
			}
			if (BenchmarkPriceType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(663);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)BenchmarkPriceType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 663);
			}
			if (BenchmarkSecurityID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(699);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BenchmarkSecurityID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 699);
			}
			if (BenchmarkSecurityIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(761);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BenchmarkSecurityIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 761);
			}
		}
	}
}
