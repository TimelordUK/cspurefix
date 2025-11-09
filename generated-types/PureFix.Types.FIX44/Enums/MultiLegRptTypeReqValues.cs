using System;

namespace PureFix.Types.FIX44
{
	public static class MultiLegRptTypeReqValues
	{
		public const int ReportByMulitlegSecurityOnly = 0;
		public const int ReportByMultilegSecurityAndInstrumentLegs = 1;
		public const int ReportByInstrumentLegsOnly = 2;
	}
}
