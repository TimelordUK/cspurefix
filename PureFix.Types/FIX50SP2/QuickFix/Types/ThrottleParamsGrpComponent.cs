using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ThrottleParamsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1610, Offset = 0, Required = false)]
		public UserResponseNoThrottles[]? NoThrottles {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoThrottles is not null && NoThrottles.Length != 0)
			{
				writer.WriteWholeNumber(1610, NoThrottles.Length);
				for (int i = 0; i < NoThrottles.Length; i++)
				{
					((IFixEncoder)NoThrottles[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoThrottles") is IMessageView viewNoThrottles)
			{
				var count = viewNoThrottles.GroupCount();
				NoThrottles = new UserResponseNoThrottles[count];
				for (int i = 0; i < count; i++)
				{
					NoThrottles[i] = new();
					((IFixParser)NoThrottles[i]).Parse(viewNoThrottles.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoThrottles":
					value = NoThrottles;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoThrottles = null;
		}
	}
}
