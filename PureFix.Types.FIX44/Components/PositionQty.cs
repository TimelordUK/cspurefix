using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class PositionQty : IFixComponent
	{
		public sealed partial class NoPositions : IFixGroup
		{
			[TagDetails(Tag = 703, Type = TagType.String, Offset = 0, Required = false)]
			public string? PosType {get; set;}
			
			[TagDetails(Tag = 704, Type = TagType.Float, Offset = 1, Required = false)]
			public double? LongQty {get; set;}
			
			[TagDetails(Tag = 705, Type = TagType.Float, Offset = 2, Required = false)]
			public double? ShortQty {get; set;}
			
			[TagDetails(Tag = 706, Type = TagType.Int, Offset = 3, Required = false)]
			public int? PosQtyStatus {get; set;}
			
			[Component(Offset = 4, Required = false)]
			public NestedParties? NestedParties {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (PosType is not null) writer.WriteString(703, PosType);
				if (LongQty is not null) writer.WriteNumber(704, LongQty.Value);
				if (ShortQty is not null) writer.WriteNumber(705, ShortQty.Value);
				if (PosQtyStatus is not null) writer.WriteWholeNumber(706, PosQtyStatus.Value);
				if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				PosType = view.GetString(703);
				LongQty = view.GetDouble(704);
				ShortQty = view.GetDouble(705);
				PosQtyStatus = view.GetInt32(706);
				if (view.GetView("NestedParties") is IMessageView viewNestedParties)
				{
					NestedParties = new();
					((IFixParser)NestedParties).Parse(viewNestedParties);
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "PosType":
					{
						value = PosType;
						break;
					}
					case "LongQty":
					{
						value = LongQty;
						break;
					}
					case "ShortQty":
					{
						value = ShortQty;
						break;
					}
					case "PosQtyStatus":
					{
						value = PosQtyStatus;
						break;
					}
					case "NestedParties":
					{
						value = NestedParties;
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
				PosType = null;
				LongQty = null;
				ShortQty = null;
				PosQtyStatus = null;
				((IFixReset?)NestedParties)?.Reset();
			}
		}
		[Group(NoOfTag = 702, Offset = 0, Required = false)]
		public NoPositions[]? Positions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Positions is not null && Positions.Length != 0)
			{
				writer.WriteWholeNumber(702, Positions.Length);
				for (int i = 0; i < Positions.Length; i++)
				{
					((IFixEncoder)Positions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPositions") is IMessageView viewNoPositions)
			{
				var count = viewNoPositions.GroupCount();
				Positions = new NoPositions[count];
				for (int i = 0; i < count; i++)
				{
					Positions[i] = new();
					((IFixParser)Positions[i]).Parse(viewNoPositions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPositions":
				{
					value = Positions;
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
			Positions = null;
		}
	}
}
