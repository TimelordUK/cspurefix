using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Types;

namespace PureFix.Test.ModularTypes.Helpers
{
    internal class SecDefHelper(IFixDefinitions definitions)
    {
        public IFixDefinitions Definitions { get; } = definitions;

        public IContainedSet GetSecListGrp()
        {
            return Definitions.GetSet("SecurityList.SecListGrp") ?? throw new InvalidOperationException();
        }

        public IContainedSet GetNumRelatedSym()
        {
            return GetSecListGrp().GetSet("NoRelatedSym") ?? throw new InvalidOperationException();
        }

        public IContainedSet GetSecurityTradingRules()
        {
            return GetNumRelatedSym().GetSet("SecurityTradingRules") ?? throw new InvalidOperationException();
        }

        public IContainedSet GetBaseTradingRules()
        {
            return GetSecurityTradingRules().GetSet("BaseTradingRules") ?? throw new InvalidOperationException();
        }

        public IContainedSet GetTickRules()
        {
            return GetBaseTradingRules().GetSet("TickRules") ?? throw new InvalidOperationException();
        }

        public IContainedSet GetNoTickRules()
        {
            return GetTickRules().GetSet("NoTickRules") ?? throw new InvalidOperationException();
        }
    }
}
