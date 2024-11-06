using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CollateralRequestNoExecs : IFixGroup
	{
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 0, Required = false)]
		public string? ExecID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ExecID is not null) writer.WriteString(17, ExecID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ExecID = view.GetString(17);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ExecID":
					value = ExecID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ExecID = null;
		}
	}
}
