using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoDerivativeInstrAttrib : IFixGroup
	{
		[TagDetails(Tag = 1313, Type = TagType.Int, Offset = 0, Required = false)]
		public int? DerivativeInstrAttribType {get; set;}
		
		[TagDetails(Tag = 1314, Type = TagType.String, Offset = 1, Required = false)]
		public string? DerivativeInstrAttribValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DerivativeInstrAttribType is not null) writer.WriteWholeNumber(1313, DerivativeInstrAttribType.Value);
			if (DerivativeInstrAttribValue is not null) writer.WriteString(1314, DerivativeInstrAttribValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DerivativeInstrAttribType = view.GetInt32(1313);
			DerivativeInstrAttribValue = view.GetString(1314);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DerivativeInstrAttribType":
					value = DerivativeInstrAttribType;
					break;
				case "DerivativeInstrAttribValue":
					value = DerivativeInstrAttribValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DerivativeInstrAttribType = null;
			DerivativeInstrAttribValue = null;
		}
	}
}
