using System.Data.SqlTypes;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.QuickFix;

public partial class QuickFixXmlFileParser
{
    public FixDefinitions Definitions { get; }


    public QuickFixXmlFileParser(FixDefinitions definitions)
    {
        Definitions = definitions;
    }

    public void Parse(string path)
    {
        var doc = XDocument.Load(path);
        ParseMessages(doc);
        // first parse all fields including their enum definitions, and add to the dictionary
        ParseFields(doc);
    }

    public void ExpandSet(, XElement element)
    {
        var descendants = element.Descendants();
        foreach (var descendant in descendants)
        {
            switch (descendant.Name.LocalName)
            {
                case "field":
                    var ad = AsAttributeDict(descendant);
                    break;
            }
        }
    }
}