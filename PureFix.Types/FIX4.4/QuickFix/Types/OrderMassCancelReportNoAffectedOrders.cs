using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrderMassCancelReportNoAffectedOrders : IFixGroup
	{
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 0, Required = false)]
		public string? OrigClOrdID {get; set;}
		
		[TagDetails(Tag = 535, Type = TagType.String, Offset = 1, Required = false)]
		public string? AffectedOrderID {get; set;}
		
		[TagDetails(Tag = 536, Type = TagType.String, Offset = 2, Required = false)]
		public string? AffectedSecondaryOrderID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (AffectedOrderID is not null) writer.WriteString(535, AffectedOrderID);
			if (AffectedSecondaryOrderID is not null) writer.WriteString(536, AffectedSecondaryOrderID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OrigClOrdID = view.GetString(41);
			AffectedOrderID = view.GetString(535);
			AffectedSecondaryOrderID = view.GetString(536);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "OrigClOrdID":
					value = OrigClOrdID;
					break;
				case "AffectedOrderID":
					value = AffectedOrderID;
					break;
				case "AffectedSecondaryOrderID":
					value = AffectedSecondaryOrderID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			OrigClOrdID = null;
			AffectedOrderID = null;
			AffectedSecondaryOrderID = null;
		}
	}
}
