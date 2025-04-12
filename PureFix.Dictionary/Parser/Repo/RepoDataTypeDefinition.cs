using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser.Repo
{
    /*
 <Datatypes xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.5.0SP2" xsi:noNamespaceSchemaLocation="../../schema/Datatypes.xsd" generated="2010-05-20T02:24:22.638-04:00" latestEP="90">
	<Datatype added="FIX.2.7" issue="SPEC-370">
		<Name>int</Name>
		<Description>Sequence of digits without commas or decimals and optional sign character (ASCII characters "-" and "0" - "9" ). The sign character utilizes one byte (i.e. positive int is "99999" while negative int is "-99999"). Note that int values may contain leading zeros (e.g. "00023" = "23").
Examples:
723 in field 21 would be mapped int as |21=723|.
-723 in field 12 would be mapped int as |12=-723|
The following data types are based on int.
</Description>
		<XML>
			<BuiltIn>1</BuiltIn>
			<Base>xs:integer</Base>
					<Description>Sequence of digits without commas or decimals and optional sign character (ASCII characters "-" and "0" - "9" ). The sign character utilizes one byte (i.e. positive int is "99999" while negative int is "-99999"). Note that int values may contain leading zeros (e.g. "00023" = "23").
Examples:
723 in field 21 would be mapped int as |21=723|.
-723 in field 12 would be mapped int as |12=-723|
The following data types are based on int.
</Description>
	*/
    public record RepoDataTypeDefinition(string Name, string Description)
    {
    }
}
