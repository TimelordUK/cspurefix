using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecutionReportNoTrdRegPublications : IFixGroup
	{
		[TagDetails(Tag = 2669, Type = TagType.Int, Offset = 0, Required = false)]
		public int? TrdRegPublicationType {get; set;}
		
		[TagDetails(Tag = 2670, Type = TagType.Int, Offset = 1, Required = false)]
		public int? TrdRegPublicationReason {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TrdRegPublicationType is not null) writer.WriteWholeNumber(2669, TrdRegPublicationType.Value);
			if (TrdRegPublicationReason is not null) writer.WriteWholeNumber(2670, TrdRegPublicationReason.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TrdRegPublicationType = view.GetInt32(2669);
			TrdRegPublicationReason = view.GetInt32(2670);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TrdRegPublicationType":
					value = TrdRegPublicationType;
					break;
				case "TrdRegPublicationReason":
					value = TrdRegPublicationReason;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TrdRegPublicationType = null;
			TrdRegPublicationReason = null;
		}
	}
}
