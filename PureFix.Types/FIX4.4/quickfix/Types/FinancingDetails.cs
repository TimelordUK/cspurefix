using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class FinancingDetails
	{
		[TagDetails(Tag = 913, Type = TagType.String, Offset = 0)]
		public string? AgreementDesc { get; set; }
		
		[TagDetails(Tag = 914, Type = TagType.String, Offset = 1)]
		public string? AgreementID { get; set; }
		
		[TagDetails(Tag = 915, Type = TagType.LocalDate, Offset = 2)]
		public DateTime? AgreementDate { get; set; }
		
		[TagDetails(Tag = 918, Type = TagType.String, Offset = 3)]
		public string? AgreementCurrency { get; set; }
		
		[TagDetails(Tag = 788, Type = TagType.Int, Offset = 4)]
		public int? TerminationType { get; set; }
		
		[TagDetails(Tag = 916, Type = TagType.LocalDate, Offset = 5)]
		public DateTime? StartDate { get; set; }
		
		[TagDetails(Tag = 917, Type = TagType.LocalDate, Offset = 6)]
		public DateTime? EndDate { get; set; }
		
		[TagDetails(Tag = 919, Type = TagType.Int, Offset = 7)]
		public int? DeliveryType { get; set; }
		
		[TagDetails(Tag = 898, Type = TagType.Float, Offset = 8)]
		public double? MarginRatio { get; set; }
		
	}
}
