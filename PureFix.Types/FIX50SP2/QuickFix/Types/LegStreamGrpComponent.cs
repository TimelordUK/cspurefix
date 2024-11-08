using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40241, Offset = 0, Required = false)]
		public IOINoLegStreams[]? NoLegStreams {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreams is not null && NoLegStreams.Length != 0)
			{
				writer.WriteWholeNumber(40241, NoLegStreams.Length);
				for (int i = 0; i < NoLegStreams.Length; i++)
				{
					((IFixEncoder)NoLegStreams[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreams") is IMessageView viewNoLegStreams)
			{
				var count = viewNoLegStreams.GroupCount();
				NoLegStreams = new IOINoLegStreams[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreams[i] = new();
					((IFixParser)NoLegStreams[i]).Parse(viewNoLegStreams.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreams":
					value = NoLegStreams;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreams = null;
		}
	}
}
