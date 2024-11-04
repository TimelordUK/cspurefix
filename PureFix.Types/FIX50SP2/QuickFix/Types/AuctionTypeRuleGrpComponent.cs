using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AuctionTypeRuleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2548, Offset = 0, Required = false)]
		public NoAuctionTypeRules[]? NoAuctionTypeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAuctionTypeRules is not null && NoAuctionTypeRules.Length != 0)
			{
				writer.WriteWholeNumber(2548, NoAuctionTypeRules.Length);
				for (int i = 0; i < NoAuctionTypeRules.Length; i++)
				{
					((IFixEncoder)NoAuctionTypeRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAuctionTypeRules") is IMessageView viewNoAuctionTypeRules)
			{
				var count = viewNoAuctionTypeRules.GroupCount();
				NoAuctionTypeRules = new NoAuctionTypeRules[count];
				for (int i = 0; i < count; i++)
				{
					NoAuctionTypeRules[i] = new();
					((IFixParser)NoAuctionTypeRules[i]).Parse(viewNoAuctionTypeRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAuctionTypeRules":
					value = NoAuctionTypeRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAuctionTypeRules = null;
		}
	}
}
