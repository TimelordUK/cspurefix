using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public static class ListExtensions
    {
        public static int BinarySearchIndexOf<T>(this IList<T> list, T value, IComparer<T>? comparer = null)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            comparer = comparer ?? Comparer<T>.Default;

            var lower = 0;
            var upper = list.Count - 1;

            while (lower <= upper)
            {
                var middle = lower + (upper - lower) / 2;
                var comparisonResult = comparer.Compare(value, list[middle]);
                switch (comparisonResult)
                {
                    case 0:
                        return middle;
                    case < 0:
                        upper = middle - 1;
                        break;
                    default:
                        lower = middle + 1;
                        break;
                }
            }

            return ~lower;
        }
    }
}
