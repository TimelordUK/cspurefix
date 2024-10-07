using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFix.Dictionary.Parser.QuickFix
{
    internal class QuickFixXmlFormatter
    {
        private static string IsRequired(bool r)
        {
            return r ? "Y" : "N";
        }

        private static string Whitespace(int n)
        {
            return new string(' ', n);
        }

        public static string StartFix(int major, int minor, int servicePack)
        {
            return $"<fix major='{major}' type='FIX' servicepack='{servicePack}' minor='{minor}'>{Environment.NewLine}";
        }
        public static string EndFix()
        {
            return $"</fix>{Environment.NewLine}";
        }

        public static string StartEntity(string name, int ws)
        {
            return $"{Whitespace(ws)}<{name}>{Environment.NewLine}";
        }

        public static string EndEntity(string name, int ws)
        {
            return $"{Whitespace(ws)}</{name}>{Environment.NewLine}";
        }

        public static string StartComponent(string name, int ws)
        {
            return $"{Whitespace(ws)}<component name = '{name}' >{Environment.NewLine}";
        }

        public static string EndComponent(int ws)
        {
            return $"{Whitespace(ws)}</component>{Environment.NewLine}";
        }

        public static string AddField(ContainedSimpleField sf, int ws)
        {
            return $"{Whitespace(ws)} <!-- {sf.Definition.Tag} {sf.Definition.Type} -->{Environment.NewLine}{Whitespace(ws)}<field name = '{sf.Name}' required = '{IsRequired(sf.Required)}' />{Environment.NewLine}";
        }

        public static string AddComponent(ContainedComponentField cf, int ws)
        {
            return $"{Whitespace(ws)}<component name = '{cf.Name}' required = '{IsRequired(cf.Required)}' />{Environment.NewLine}";
        }

        public static string AddGroup(ContainedGroupField gf, int ws)
        {
            return $"{Whitespace(ws)} <!-- {gf?.Definition?.NoOfField?.Tag ?? -1} {gf?.Definition?.NoOfField?.Type} -->{Environment.NewLine} {Whitespace(ws)}<group name = '{gf?.Name}' required = '{IsRequired(gf?.Required ?? false)}'>{Environment.NewLine}";
        }

        public static string EndGroup(int ws)
        {
            return $"{Whitespace(ws)}</group>{Environment.NewLine}";
        }

        public static string AddEnum(FieldEnum fe, int ws)
        {
            return $"{Whitespace(ws)}<value enum= '{fe?.Key}' description = '{fe?.Description ?? fe?.Val}' />{Environment.NewLine}";
        }

        public static string DefineField(SimpleFieldDefinition sf, int ws)
        {
            var term = sf.IsEnum ? ">" : "/>";
            return $"{Whitespace(ws)}<field number = '{sf.Tag}' name = '{sf.Name}' type = '{sf.Type}'{term}{Environment.NewLine}";
        }

        public static string DefineMessage(MessageDefinition def, int ws)
        {
            return $"{Whitespace(ws)}<message name = '{def.Name}' msgcat = '{def.Category}' msgtype = '{def.MsgType}' >{Environment.NewLine}";
        }
    }
}
