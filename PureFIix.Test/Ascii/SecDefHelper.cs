using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;

namespace PureFIix.Test.Ascii
{
    internal class SecDefHelper(FixDefinitions definitions)
    {
        public FixDefinitions Definitions { get; } = definitions;

        public IContainedSet GetSecListGrp()
        {
            return Definitions.GetSet("SecurityList.SecListGrp");
        }

        public IContainedSet GetNumRelatedSym()
        {
            return GetSecListGrp().GetSet("NoRelatedSym");
        }

        public IContainedSet GetSecurityTradingRules()
        {
            return GetNumRelatedSym().GetSet("SecurityTradingRules");
        }

        public IContainedSet GetBaseTradingRules()
        {
            return GetSecurityTradingRules().GetSet("BaseTradingRules");
        }

        public IContainedSet GetTickRules()
        {
            return GetBaseTradingRules().GetSet("TickRules");
        }

        public IContainedSet GetNoTickRules()
        {
            return GetTickRules().GetSet("NoTickRules");
        }
    }
}
