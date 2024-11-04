using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class NoExecs : IFixGroup
	{
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 0, Required = false)]
		public double? LastShares {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 1, Required = false)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 2, Required = false)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 3, Required = false)]
		public string? LastCapacity {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LastShares is not null) writer.WriteNumber(32, LastShares.Value);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (LastCapacity is not null) writer.WriteString(29, LastCapacity);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LastShares = view.GetDouble(32);
			ExecID = view.GetString(17);
			LastPx = view.GetDouble(31);
			LastCapacity = view.GetString(29);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LastShares":
					value = LastShares;
					break;
				case "ExecID":
					value = ExecID;
					break;
				case "LastPx":
					value = LastPx;
					break;
				case "LastCapacity":
					value = LastCapacity;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LastShares = null;
			ExecID = null;
			LastPx = null;
			LastCapacity = null;
		}
	}
}
