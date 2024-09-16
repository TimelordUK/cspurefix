using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class StandardTrailerExt
	{
		public static void Parse(this StandardTrailer instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SignatureLength = view.GetInt32(93);
			instance.Signature = view.GetByteArray(89);
			instance.CheckSum = view.GetString(10);
		}
	}
}
