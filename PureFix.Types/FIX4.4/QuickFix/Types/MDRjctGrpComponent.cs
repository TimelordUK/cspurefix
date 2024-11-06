using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDRjctGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 816, Offset = 0, Required = false)]
		public MarketDataRequestRejectNoAltMDSource[]? NoAltMDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAltMDSource is not null && NoAltMDSource.Length != 0)
			{
				writer.WriteWholeNumber(816, NoAltMDSource.Length);
				for (int i = 0; i < NoAltMDSource.Length; i++)
				{
					((IFixEncoder)NoAltMDSource[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAltMDSource") is IMessageView viewNoAltMDSource)
			{
				var count = viewNoAltMDSource.GroupCount();
				NoAltMDSource = new MarketDataRequestRejectNoAltMDSource[count];
				for (int i = 0; i < count; i++)
				{
					NoAltMDSource[i] = new();
					((IFixParser)NoAltMDSource[i]).Parse(viewNoAltMDSource.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAltMDSource":
					value = NoAltMDSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAltMDSource = null;
		}
	}
}
