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
		[TagDetails(372)]
		public string? RefMsgType { get; set; } // STRING
		
		[TagDetails(385)]
		public string? MsgDirection { get; set; } // CHAR
		
	}
}
