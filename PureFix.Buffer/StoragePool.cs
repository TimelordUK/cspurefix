using Microsoft.Extensions.ObjectPool;
using PureFix.Types;
using PureFix.Types.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer
{
    public class StoragePool
    {
        public class Storage
        {
            public ElasticBuffer Buffer { get; } = new();
            public Tags Locations { get; } = new();

            public TagPos? BeginStringLoc => Locations?[0];
            public TagPos? BodyLengthLoc => Locations?[1];
            public TagPos? MsgTypeLoc => Locations?[2];

            public void Reset()
            {
                Buffer.Reset();
                Locations.Reset();
            }

            public string? GetStringAt(int pos)
            {
                if (pos < 0 || pos >= Locations.Count) return null;
                var l = Locations[pos];
                return Buffer.GetString(l.Start, l.End + 1);
            }

            // used for fix log to return string with a write delimiter 
            public string AsString(byte delim)
            {
                var b = Buffer.Clone();
                var p = b.Pos;
                for (var i = 0; i < Locations.Count; i++)
                {
                    var l = Locations[i];
                    b.SetPos(l.End + 1);
                    b.WriteChar(delim);
                }

                b.SetPos(p);
                return b.ToString();
            }

            public int Checksum()
            {
                return Buffer.Checksum();
            }

            public void PatchBodyLength(int width)
            {
                var bodyLenPos = BodyLengthLoc;
                if (bodyLenPos == null) return;
                var pos = Buffer.Pos;
                var writePtr = bodyLenPos.Value.Start;
                // measure from start of the msgtag field - which is 
                // the end of previous field (bodylen) plus the delim
                // end is last value position need to go forwards the delim
                var bodyLen = pos - (bodyLenPos.Value.End + 2);
                Buffer.SetPos(writePtr);
                Buffer.WriteLeadingZeroes(bodyLen, width);
                Buffer.SetPos(pos);
            }

            public byte[] AsBytes()
            {
                return Buffer.Clone().GetBytes();
            }

            public override string ToString()
            {
                return $"[{Buffer.Pos}, {Locations.NextTagPos}]";
            }
        }
        
        public Storage Rent()
        {
            var instance = _pool.Get();
            instance.Reset();
            ++Rents;
            return instance;
        }

        public void Return(Storage storage)
        {
            Returns++;
            _pool.Return(storage);
        }
      
        public long Imbalance => Rents - Returns;
        public long Rents { get; private set; }

        public long Returns { get; private set; }

        private readonly ObjectPool<Storage> _pool = new DefaultObjectPool<Storage>(new DefaultPooledObjectPolicy<Storage>());
    }
}
