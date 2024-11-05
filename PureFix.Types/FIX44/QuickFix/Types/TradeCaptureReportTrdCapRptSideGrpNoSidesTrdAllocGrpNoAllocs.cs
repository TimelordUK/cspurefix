using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TradeCaptureReportTrdCapRptSideGrpNoSidesTrdAllocGrpNoAllocs : IFixGroup
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount {get; set;}
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 1, Required = false)]
		public int? AllocAcctIDSource {get; set;}
		
		[TagDetails(Tag = 736, Type = TagType.String, Offset = 2, Required = false)]
		public string? AllocSettlCurrency {get; set;}
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 3, Required = false)]
		public string? IndividualAllocID {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public NestedParties2Component? NestedParties2 {get; set;}
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 5, Required = false)]
		public double? AllocQty {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (AllocAcctIDSource is not null) writer.WriteWholeNumber(661, AllocAcctIDSource.Value);
			if (AllocSettlCurrency is not null) writer.WriteString(736, AllocSettlCurrency);
			if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
			if (NestedParties2 is not null) ((IFixEncoder)NestedParties2).Encode(writer);
			if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AllocAccount = view.GetString(79);
			AllocAcctIDSource = view.GetInt32(661);
			AllocSettlCurrency = view.GetString(736);
			IndividualAllocID = view.GetString(467);
			if (view.GetView("NestedParties2") is IMessageView viewNestedParties2)
			{
				NestedParties2 = new();
				((IFixParser)NestedParties2).Parse(viewNestedParties2);
			}
			AllocQty = view.GetDouble(80);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AllocAccount":
					value = AllocAccount;
					break;
				case "AllocAcctIDSource":
					value = AllocAcctIDSource;
					break;
				case "AllocSettlCurrency":
					value = AllocSettlCurrency;
					break;
				case "IndividualAllocID":
					value = IndividualAllocID;
					break;
				case "NestedParties2":
					value = NestedParties2;
					break;
				case "AllocQty":
					value = AllocQty;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			AllocAccount = null;
			AllocAcctIDSource = null;
			AllocSettlCurrency = null;
			IndividualAllocID = null;
			((IFixReset?)NestedParties2)?.Reset();
			AllocQty = null;
		}
	}
}
