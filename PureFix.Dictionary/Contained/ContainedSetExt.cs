using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public interface ISetDispatchReceiver
    {
        void OnSimple(ContainedSimpleField sf);
        void OnComponent(ContainedComponentField cf);
        void OnGroup(ContainedGroupField cf);
    }
    public static class ContainedSetExt
    {
        public static void Iterate(this IContainedSet set, ISetDispatchReceiver dispatcher)
        {
            foreach (var k in set.Fields)
            {
                switch (k.Type)
                {
                    case ContainedFieldType.Simple:
                        dispatcher.OnSimple((ContainedSimpleField)k);
                        break;

                    case ContainedFieldType.Group:
                        dispatcher.OnGroup((ContainedGroupField)k);
                        break;

                    case ContainedFieldType.Component:
                        dispatcher.OnComponent((ContainedComponentField)k);
                        break;
                }
            }
        }
    }
}
