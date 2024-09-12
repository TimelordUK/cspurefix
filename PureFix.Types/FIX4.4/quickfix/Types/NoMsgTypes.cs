using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoMsgTypes
	{
		[TagDetails(372, TagType.String)]
		public string? RefMsgType { get; set; }
		
		[TagDetails(385, TagType.String)]
		public string? MsgDirection { get; set; }
		
	}
}
