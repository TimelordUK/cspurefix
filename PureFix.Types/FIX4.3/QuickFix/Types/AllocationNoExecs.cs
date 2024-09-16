using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class AllocationNoExecs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 0, Required = false)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 1, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 3, Required = false)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 4, Required = false)]
		public string? LastCapacity { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (LastCapacity is not null) writer.WriteString(29, LastCapacity);
		}
	}
}
