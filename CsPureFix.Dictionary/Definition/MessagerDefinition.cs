using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Definition
{
    public class MessageDefinition : ContainedFieldSet
    {
        public string MsgType { get; }
        public MessageDefinition(string name, string abbreviation, string msgType, string category, string description) :
            base(ContainedSetType.Msg, name, category, abbreviation, description)
        {
            MsgType = msgType;
        }

        public override string GetPrefix()
        {
            return $"M.{MsgType}";
        }
    }
}
