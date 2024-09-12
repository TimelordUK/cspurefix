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
		[TagDetails(913, TagType.String)]
		public string? AgreementDesc { get; set; }
		
		[TagDetails(914, TagType.String)]
		public string? AgreementID { get; set; }
		
		[TagDetails(915, TagType.LocalDate)]
		public DateTime? AgreementDate { get; set; }
		
		[TagDetails(918, TagType.String)]
		public string? AgreementCurrency { get; set; }
		
		[TagDetails(788, TagType.Int)]
		public int? TerminationType { get; set; }
		
		[TagDetails(916, TagType.LocalDate)]
		public DateTime? StartDate { get; set; }
		
		[TagDetails(917, TagType.LocalDate)]
		public DateTime? EndDate { get; set; }
		
		[TagDetails(919, TagType.Int)]
		public int? DeliveryType { get; set; }
		
		[TagDetails(898, TagType.Float)]
		public double? MarginRatio { get; set; }
		
	}
}
