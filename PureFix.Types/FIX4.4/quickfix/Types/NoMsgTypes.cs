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
		public string? RefMsgType { get; set; } // 372 STRING
		public string? MsgDirection { get; set; } // 385 CHAR
	}
}
