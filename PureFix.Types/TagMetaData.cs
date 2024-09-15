using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public readonly struct TagMetaData
    {
        public required TagType TagType{get; init;}
        public required Type Type{get; init;}
        public required string TypeName{get; init;}
        public required string Getter{get; init;}
        public required string Writer{get; init;}
    }
}
