using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser.Repo
{
    /*
     * 	<Message added="FIX.4.1">
		<ComponentID>28</ComponentID>
		<MsgType>T</MsgType>
		<Name>SettlementInstructions</Name>
		<CategoryID>SettlementInstruction</CategoryID>
		<SectionID>PostTrade</SectionID>
		<AbbrName>SettlInstrctns</AbbrName>
		<NotReqXML>0</NotReqXML>
		<Description>The Settlement Instructions message provides the broker’s, the institution’s, or the intermediary’s instructions for trade settlement. This message has been designed so that it can be sent from the broker to the institution, from the institution to the broker, or from either to an independent "standing instructions" database or matching system or, for CIV, from an intermediary to a fund manager.</Description>
	</Message>
     */
    public record RepoMessageDefinition(int ComponentID, string MsgType, string Name, string CategoryID, string SectionID, string AbbrName, int NotReqXML, string Description)
    {
    }
}
