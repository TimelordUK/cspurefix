using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IStandardHeader
    {
        public string? MsgType{get;}
        public int? BodyLength{get; }
        public string? SenderCompID { get; }
        public string? TargetCompID { get; }
        public string? TargetSubID { get; }
        public string? BeginString { get; }
    }
}
