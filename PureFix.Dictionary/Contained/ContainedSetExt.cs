using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public static class ContainedSetExt
    {
        public static void Iterate(this IContainedSet set, ISetDispatchReceiver dispatcher)
        {
            dispatcher.PreIterate(set);

            var fields = set.Fields;
            for (var i = 0; i < fields.Count; i++)
            {
                var k = fields[i];

                object? peek = null;

                if (i < fields.Count - 1)
                {
                    peek = fields[i + 1];
                }

                switch (k.Type)
                {
                    case ContainedFieldType.Simple:
                        dispatcher.OnSimple((ContainedSimpleField)k, i, peek);
                        break;

                    case ContainedFieldType.Group:
                        dispatcher.OnGroup((ContainedGroupField)k, i);
                        break;

                    case ContainedFieldType.Component:
                        dispatcher.OnComponent((ContainedComponentField)k, i);
                        break;
                }
            }

            dispatcher.PostIterate(set);
        }
    }
}
