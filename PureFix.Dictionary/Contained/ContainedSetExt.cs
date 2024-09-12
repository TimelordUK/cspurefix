using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public interface ISetDispatchReceiver
    {
        void OnSimple(ContainedSimpleField sf, object? state = null);
        void OnComponent(ContainedComponentField cf, object? state = null);
        void OnGroup(ContainedGroupField cf, object? state = null);
    }
    public static class ContainedSetExt
    {
        public static void Iterate(this IContainedSet set, ISetDispatchReceiver dispatcher, object? state = null)
        {
            var fields = set.Fields;
            for (var i = 0; i < fields.Count; i++)
            {
                var k = fields[i];

                switch (k.Type)
                {
                    case ContainedFieldType.Simple:
                        dispatcher.OnSimple((ContainedSimpleField)k, state);
                        break;

                    case ContainedFieldType.Group:
                        dispatcher.OnGroup((ContainedGroupField)k, state);
                        break;

                    case ContainedFieldType.Component:
                        dispatcher.OnComponent((ContainedComponentField)k, state);
                        break;
                }
            }
        }
    }
}
