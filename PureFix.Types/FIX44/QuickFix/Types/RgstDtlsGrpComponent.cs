using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class RgstDtlsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 473, Offset = 0, Required = false)]
		public RegistrationInstructionsRgstDtlsGrpNoRegistDtls[]? NoRegistDtls {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRegistDtls is not null && NoRegistDtls.Length != 0)
			{
				writer.WriteWholeNumber(473, NoRegistDtls.Length);
				for (int i = 0; i < NoRegistDtls.Length; i++)
				{
					((IFixEncoder)NoRegistDtls[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRegistDtls") is IMessageView viewNoRegistDtls)
			{
				var count = viewNoRegistDtls.GroupCount();
				NoRegistDtls = new RegistrationInstructionsRgstDtlsGrpNoRegistDtls[count];
				for (int i = 0; i < count; i++)
				{
					NoRegistDtls[i] = new();
					((IFixParser)NoRegistDtls[i]).Parse(viewNoRegistDtls.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRegistDtls":
					value = NoRegistDtls;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRegistDtls = null;
		}
	}
}
