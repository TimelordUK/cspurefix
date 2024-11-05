using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NewOrderMultilegLegOrdGrpNoLegsLegPreAllocGrpNoLegAllocs : IFixGroup
	{
		[TagDetails(Tag = 671, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegAllocAccount {get; set;}
		
		[TagDetails(Tag = 672, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegIndividualAllocID {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public NestedParties2Component? NestedParties2 {get; set;}
		
		[TagDetails(Tag = 673, Type = TagType.Float, Offset = 3, Required = false)]
		public double? LegAllocQty {get; set;}
		
		[TagDetails(Tag = 674, Type = TagType.String, Offset = 4, Required = false)]
		public string? LegAllocAcctIDSource {get; set;}
		
		[TagDetails(Tag = 675, Type = TagType.String, Offset = 5, Required = false)]
		public string? LegSettlCurrency {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegAllocAccount is not null) writer.WriteString(671, LegAllocAccount);
			if (LegIndividualAllocID is not null) writer.WriteString(672, LegIndividualAllocID);
			if (NestedParties2 is not null) ((IFixEncoder)NestedParties2).Encode(writer);
			if (LegAllocQty is not null) writer.WriteNumber(673, LegAllocQty.Value);
			if (LegAllocAcctIDSource is not null) writer.WriteString(674, LegAllocAcctIDSource);
			if (LegSettlCurrency is not null) writer.WriteString(675, LegSettlCurrency);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegAllocAccount = view.GetString(671);
			LegIndividualAllocID = view.GetString(672);
			if (view.GetView("NestedParties2") is IMessageView viewNestedParties2)
			{
				NestedParties2 = new();
				((IFixParser)NestedParties2).Parse(viewNestedParties2);
			}
			LegAllocQty = view.GetDouble(673);
			LegAllocAcctIDSource = view.GetString(674);
			LegSettlCurrency = view.GetString(675);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegAllocAccount":
					value = LegAllocAccount;
					break;
				case "LegIndividualAllocID":
					value = LegIndividualAllocID;
					break;
				case "NestedParties2":
					value = NestedParties2;
					break;
				case "LegAllocQty":
					value = LegAllocQty;
					break;
				case "LegAllocAcctIDSource":
					value = LegAllocAcctIDSource;
					break;
				case "LegSettlCurrency":
					value = LegSettlCurrency;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegAllocAccount = null;
			LegIndividualAllocID = null;
			((IFixReset?)NestedParties2)?.Reset();
			LegAllocQty = null;
			LegAllocAcctIDSource = null;
			LegSettlCurrency = null;
		}
	}
}
