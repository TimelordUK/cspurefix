using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ExecCollGrpNoExecs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 0, Required = false)]
		public string? ExecID { get; set; }
		
	}
}
