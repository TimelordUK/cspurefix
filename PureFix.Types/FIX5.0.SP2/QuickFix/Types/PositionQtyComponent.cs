using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PositionQtyComponent : IFixComponent
	{
		[Group(NoOfTag = 702, Offset = 0, Required = false)]
		public NoPositions[]? NoPositions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPositions is not null && NoPositions.Length != 0)
			{
				writer.WriteWholeNumber(702, NoPositions.Length);
				for (int i = 0; i < NoPositions.Length; i++)
				{
					((IFixEncoder)NoPositions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPositions") is IMessageView viewNoPositions)
			{
				var count = viewNoPositions.GroupCount();
				NoPositions = new NoPositions[count];
				for (int i = 0; i < count; i++)
				{
					NoPositions[i] = new();
					((IFixParser)NoPositions[i]).Parse(viewNoPositions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPositions":
					value = NoPositions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
