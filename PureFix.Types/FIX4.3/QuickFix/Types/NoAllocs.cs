using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class NoAllocs : IFixGroup
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount {get; set;}
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 1, Required = false)]
		public string? IndividualAllocID {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public NestedPartiesComponent? NestedParties {get; set;}
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 3, Required = false)]
		public double? AllocQty {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AllocAccount = view.GetString(79);
			IndividualAllocID = view.GetString(467);
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				NestedParties = new();
				((IFixParser)NestedParties).Parse(viewNestedParties);
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
				case "IndividualAllocID":
					value = IndividualAllocID;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				case "AllocQty":
					value = AllocQty;
					break;
				default: return false;
			}
			return true;
		}
	}
}
