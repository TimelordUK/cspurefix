using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser.Repo
{
    /*
     *  <Component added="FIX.4.3">
		    <ComponentID>1000</ComponentID>
		    <ComponentType>Block</ComponentType>
		    <CategoryID>Common</CategoryID>
		    <Name>CommissionData</Name>
		    <AbbrName>Comm</AbbrName>
		    <NotReqXML>0</NotReqXML>
		    <Description>The CommissionDate component block is used to carry commission information such as the type of commission and the rate.</Description>
	  </Component>
	*/
    public record RepoComponentDefinition(int ID, string Type, string CategoryID, string Name, string AbbrName, int NotReqXML, string Description)
    {
    }
}
