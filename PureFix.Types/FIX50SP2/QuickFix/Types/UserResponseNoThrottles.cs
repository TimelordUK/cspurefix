using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UserResponseNoThrottles : IFixGroup
	{
		[TagDetails(Tag = 1611, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ThrottleAction {get; set;}
		
		[TagDetails(Tag = 1612, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ThrottleType {get; set;}
		
		[TagDetails(Tag = 1613, Type = TagType.Int, Offset = 2, Required = false)]
		public int? ThrottleNoMsgs {get; set;}
		
		[TagDetails(Tag = 1614, Type = TagType.Int, Offset = 3, Required = false)]
		public int? ThrottleTimeInterval {get; set;}
		
		[TagDetails(Tag = 1615, Type = TagType.Int, Offset = 4, Required = false)]
		public int? ThrottleTimeUnit {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public ThrottleMsgTypeGrpComponent? ThrottleMsgTypeGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ThrottleAction is not null) writer.WriteWholeNumber(1611, ThrottleAction.Value);
			if (ThrottleType is not null) writer.WriteWholeNumber(1612, ThrottleType.Value);
			if (ThrottleNoMsgs is not null) writer.WriteWholeNumber(1613, ThrottleNoMsgs.Value);
			if (ThrottleTimeInterval is not null) writer.WriteWholeNumber(1614, ThrottleTimeInterval.Value);
			if (ThrottleTimeUnit is not null) writer.WriteWholeNumber(1615, ThrottleTimeUnit.Value);
			if (ThrottleMsgTypeGrp is not null) ((IFixEncoder)ThrottleMsgTypeGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ThrottleAction = view.GetInt32(1611);
			ThrottleType = view.GetInt32(1612);
			ThrottleNoMsgs = view.GetInt32(1613);
			ThrottleTimeInterval = view.GetInt32(1614);
			ThrottleTimeUnit = view.GetInt32(1615);
			if (view.GetView("ThrottleMsgTypeGrp") is IMessageView viewThrottleMsgTypeGrp)
			{
				ThrottleMsgTypeGrp = new();
				((IFixParser)ThrottleMsgTypeGrp).Parse(viewThrottleMsgTypeGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ThrottleAction":
					value = ThrottleAction;
					break;
				case "ThrottleType":
					value = ThrottleType;
					break;
				case "ThrottleNoMsgs":
					value = ThrottleNoMsgs;
					break;
				case "ThrottleTimeInterval":
					value = ThrottleTimeInterval;
					break;
				case "ThrottleTimeUnit":
					value = ThrottleTimeUnit;
					break;
				case "ThrottleMsgTypeGrp":
					value = ThrottleMsgTypeGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ThrottleAction = null;
			ThrottleType = null;
			ThrottleNoMsgs = null;
			ThrottleTimeInterval = null;
			ThrottleTimeUnit = null;
			((IFixReset?)ThrottleMsgTypeGrp)?.Reset();
		}
	}
}
