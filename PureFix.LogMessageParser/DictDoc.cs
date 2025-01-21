using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.LogMessageParser
{
    public record FieldMeta(int Fid, string Name, string Description);
    public record MessageMeta(string Type, string Name, string Description);

    public class DictDoc
    {
        public List<FieldMeta> Fields { get; set; } = [];
        public List<MessageMeta> Messages { get; set; } = [];
    }
}
