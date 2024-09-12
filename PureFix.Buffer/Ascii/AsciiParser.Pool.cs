using Microsoft.Extensions.ObjectPool;
using PureFix.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public partial class AsciiParser
    {
        public class Pool
        {
            public class Storage
            {
                public ElasticBuffer Buffer { get; } = new();
                public Tags Locations { get; } = new();

                public void Reset()
                {
                    Buffer.Reset();
                    Locations.Reset();
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
            public void Deliver(Storage storage)
            {
                _delivered.Enqueue(storage);
            }
            public void Reclaim()
            {
                while (_delivered.Count > 0)
                {
                    Returns++;
                    var instance = _delivered.Dequeue();
                    _pool.Return(instance);
                }
            }
            public long Imbalance => Rents - Returns;
            public long Rents { get; private set; }

            public long Returns { get; private set; }

            private readonly ObjectPool<Storage> _pool = new DefaultObjectPool<Storage>(new DefaultPooledObjectPolicy<Storage>());
            private readonly Queue<Storage> _delivered = [];
        }
    }
}
