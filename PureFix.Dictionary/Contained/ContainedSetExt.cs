using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public interface ISetDispatchReceiver
    {
        void OnSimple(ContainedSimpleField sf, int index);
        void OnComponent(ContainedComponentField cf, int index);
        void OnGroup(ContainedGroupField cf, int index);
    }
    public static class ContainedSetExt
    {
        public static void Iterate(this IContainedSet set, ISetDispatchReceiver dispatcher)
        {
            var fields = set.Fields;
            for (var i = 0; i < fields.Count; i++)
            {
                var k = fields[i];

                switch (k.Type)
                {
                    case ContainedFieldType.Simple:
                        dispatcher.OnSimple((ContainedSimpleField)k, i);
                        break;

                    case ContainedFieldType.Group:
                        dispatcher.OnGroup((ContainedGroupField)k, i);
                        break;

                    case ContainedFieldType.Component:
                        dispatcher.OnComponent((ContainedComponentField)k, i);
                        break;
                }
            }
        }
    }
}
