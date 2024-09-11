using PureFix.Buffer.Ascii;
using PureFix.Types.FIX4._4.quickfix.set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ParserFormat
{
    public static class StandardTrailerExt
    {
        public static void Parse(this StandardTrailer instance, MsgView? view)
        {
            instance.SignatureLength = view?.GetTyped<int>(93);
            instance.Signature = view?.GetTyped<byte[]>(89);
            instance.CheckSum = view?.GetTyped<string>(10);
        }
    }
}
