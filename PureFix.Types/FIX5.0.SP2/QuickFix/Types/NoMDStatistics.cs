using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMDStatistics : IFixGroup
	{
		[TagDetails(Tag = 2475, Type = TagType.String, Offset = 0, Required = false)]
		public string? MDStatisticID {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public MDStatisticParametersComponent? MDStatisticParameters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MDStatisticID is not null) writer.WriteString(2475, MDStatisticID);
			if (MDStatisticParameters is not null) ((IFixEncoder)MDStatisticParameters).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MDStatisticID = view.GetString(2475);
			if (view.GetView("MDStatisticParameters") is IMessageView viewMDStatisticParameters)
			{
				MDStatisticParameters = new();
				((IFixParser)MDStatisticParameters).Parse(viewMDStatisticParameters);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MDStatisticID":
					value = MDStatisticID;
					break;
				case "MDStatisticParameters":
					value = MDStatisticParameters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
