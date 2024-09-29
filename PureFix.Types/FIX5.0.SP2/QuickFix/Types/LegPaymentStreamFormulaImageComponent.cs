using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamFormulaImageComponent : IFixComponent
	{
		[TagDetails(Tag = 42451, Type = TagType.Length, Offset = 0, Required = false, LinksToTag = 42452)]
		public int? LegPaymentStreamFormulaImageLength {get; set;}
		
		[TagDetails(Tag = 42452, Type = TagType.RawData, Offset = 1, Required = false, LinksToTag = 42451)]
		public byte[]? LegPaymentStreamFormulaImage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamFormulaImage is not null)
			{
				writer.WriteWholeNumber(42451, LegPaymentStreamFormulaImage.Length);
				writer.WriteBuffer(42452, LegPaymentStreamFormulaImage);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamFormulaImageLength = view.GetInt32(42451);
			LegPaymentStreamFormulaImage = view.GetByteArray(42452);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamFormulaImageLength":
					value = LegPaymentStreamFormulaImageLength;
					break;
				case "LegPaymentStreamFormulaImage":
					value = LegPaymentStreamFormulaImage;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentStreamFormulaImageLength = null;
			LegPaymentStreamFormulaImage = null;
		}
	}
}
