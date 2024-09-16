using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40540, Offset = 0, Required = false)]
		public NoUnderlyingStreams[]? NoUnderlyingStreams {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreams is not null && NoUnderlyingStreams.Length != 0)
			{
				writer.WriteWholeNumber(40540, NoUnderlyingStreams.Length);
				for (int i = 0; i < NoUnderlyingStreams.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreams[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreams") is IMessageView viewNoUnderlyingStreams)
			{
				var count = viewNoUnderlyingStreams.GroupCount();
				NoUnderlyingStreams = new NoUnderlyingStreams[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreams[i] = new();
					((IFixParser)NoUnderlyingStreams[i]).Parse(viewNoUnderlyingStreams.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreams":
					value = NoUnderlyingStreams;
					break;
				default: return false;
			}
			return true;
		}
	}
}
