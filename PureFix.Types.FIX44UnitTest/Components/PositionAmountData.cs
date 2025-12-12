using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class PositionAmountData : IFixComponent
	{
		public sealed partial class NoPosAmt : IFixGroup
		{
			[TagDetails(Tag = 707, Type = TagType.String, Offset = 0, Required = false)]
			public string? PosAmtType {get; set;}
			
			[TagDetails(Tag = 708, Type = TagType.Float, Offset = 1, Required = false)]
			public double? PosAmt {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (PosAmtType is not null) writer.WriteString(707, PosAmtType);
				if (PosAmt is not null) writer.WriteNumber(708, PosAmt.Value);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				PosAmtType = view.GetString(707);
				PosAmt = view.GetDouble(708);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "PosAmtType":
					{
						value = PosAmtType;
						break;
					}
					case "PosAmt":
					{
						value = PosAmt;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				PosAmtType = null;
				PosAmt = null;
			}
		}
		[Group(NoOfTag = 753, Offset = 0, Required = false)]
		public NoPosAmt[]? PosAmt {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PosAmt is not null && PosAmt.Length != 0)
			{
				writer.WriteWholeNumber(753, PosAmt.Length);
				for (int i = 0; i < PosAmt.Length; i++)
				{
					((IFixEncoder)PosAmt[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPosAmt") is IMessageView viewNoPosAmt)
			{
				var count = viewNoPosAmt.GroupCount();
				PosAmt = new NoPosAmt[count];
				for (int i = 0; i < count; i++)
				{
					PosAmt[i] = new();
					((IFixParser)PosAmt[i]).Parse(viewNoPosAmt.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPosAmt":
				{
					value = PosAmt;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PosAmt = null;
		}
	}
}
