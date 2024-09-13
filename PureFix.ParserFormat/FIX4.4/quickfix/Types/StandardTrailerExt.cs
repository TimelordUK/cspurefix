using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class StandardTrailerExt
	{
		public static void Parse(this StandardTrailer instance, MsgView? view)
		{
			instance.SignatureLength = view?.GetInt32(93);
			instance.Signature = view?.GetByteArray(89);
			instance.CheckSum = view?.GetString(10);
		}
	}
}
