using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Definition
{
    public class MessageDefinition(
        string name,
        string abbreviation,
        string msgType,
        string category,
        string description)
        : ContainedFieldSet(ContainedSetType.Msg, name, category, abbreviation, description)
    {
        public string MsgType { get; } = msgType;

        public override string GetPrefix()
        {
            return $"M.{MsgType}";
        }
    }
}
