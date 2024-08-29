using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.tag
{
    public enum TagType
    {
        String = 1,
        Int = 2,
        Float = 3,
        Boolean = 4,
        UtcTimestamp = 5,
        UtcDateOnly = 6,
        UtcTimeOnly = 7,
        LocalDate = 8,
        RawData = 9,
        Length = 10,
        Unknown = 11
    }

    public static class TagTypeUtil
    {
        public static TagType ToType(string type)
        {
            type = type ?? "string";
            switch (type.ToLower()) {
                case "currency":
                case "string":
                case "char":
                {
                    return TagType.String;
                }

                case "int":
                case "numingroup":
                case "seqnum":
                {
                    return TagType.Int;
                }

                case "qty":
                case "percentage":
                case "amt":
                case "price":
                case "priceoffset":
                case "float":
                {
                    return TagType.Float;
                }

                case "length":
                {
                    return TagType.Length;
                }

                case "boolean":
                {
                    return TagType.Boolean;
                }

                case "utctimestamp":
                {
                    return TagType.UtcTimestamp;
                }

                case "localmktdate":
                {
                    return TagType.LocalDate;
                }

                case "utcdateonly":
                {
                    return TagType.UtcDateOnly;
                }

                case "utctimeonly":
                {
                    return TagType.UtcTimeOnly;
                }

                case "data":
                {
                    return TagType.RawData;
                }

                default:
                {
                    return TagType.String;
                }
            }
        }
    }
}
