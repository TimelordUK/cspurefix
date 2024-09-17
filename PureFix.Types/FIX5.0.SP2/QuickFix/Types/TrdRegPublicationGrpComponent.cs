using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TrdRegPublicationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2668, Offset = 0, Required = false)]
		public NoTrdRegPublications[]? NoTrdRegPublications {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrdRegPublications is not null && NoTrdRegPublications.Length != 0)
			{
				writer.WriteWholeNumber(2668, NoTrdRegPublications.Length);
				for (int i = 0; i < NoTrdRegPublications.Length; i++)
				{
					((IFixEncoder)NoTrdRegPublications[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTrdRegPublications") is IMessageView viewNoTrdRegPublications)
			{
				var count = viewNoTrdRegPublications.GroupCount();
				NoTrdRegPublications = new NoTrdRegPublications[count];
				for (int i = 0; i < count; i++)
				{
					NoTrdRegPublications[i] = new();
					((IFixParser)NoTrdRegPublications[i]).Parse(viewNoTrdRegPublications.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTrdRegPublications":
					value = NoTrdRegPublications;
					break;
				default: return false;
			}
			return true;
		}
	}
}
