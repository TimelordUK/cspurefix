using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RgstDistInstGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 510, Offset = 0, Required = false)]
		public RegistrationInstructionsNoDistribInsts[]? NoDistribInsts {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDistribInsts is not null && NoDistribInsts.Length != 0)
			{
				writer.WriteWholeNumber(510, NoDistribInsts.Length);
				for (int i = 0; i < NoDistribInsts.Length; i++)
				{
					((IFixEncoder)NoDistribInsts[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDistribInsts") is IMessageView viewNoDistribInsts)
			{
				var count = viewNoDistribInsts.GroupCount();
				NoDistribInsts = new RegistrationInstructionsNoDistribInsts[count];
				for (int i = 0; i < count; i++)
				{
					NoDistribInsts[i] = new();
					((IFixParser)NoDistribInsts[i]).Parse(viewNoDistribInsts.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDistribInsts":
					value = NoDistribInsts;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDistribInsts = null;
		}
	}
}
