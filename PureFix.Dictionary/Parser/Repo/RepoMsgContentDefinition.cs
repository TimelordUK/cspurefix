using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser.Repo
{
    /*
     * MsgContents xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.5.0SP2" xsi:noNamespaceSchemaLocation="../../schema/MsgContents.xsd" generated="2010-05-20T02:30:02.802-04:00" latestEP="95">
	<MsgContent added="FIX.2.7">
		<ComponentID>1</ComponentID>
		<TagText>StandardHeader</TagText>
		<Indent>0</Indent>
		<Position>1</Position>
		<Reqd>1</Reqd>
		<Description>MsgType = 0</Description>
	</MsgContent>
     */
    public record RepoMsgContentDefinition(int ComponentID, string TagText, int Indent, string Position, int Required, string Description)
    {
    }
}
