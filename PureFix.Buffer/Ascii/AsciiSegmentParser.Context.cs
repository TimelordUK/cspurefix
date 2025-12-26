using Microsoft.Extensions.ObjectPool;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public partial class AsciiSegmentParser
    {
        public class Context : IDisposable
        {
            private static readonly ObjectPool<Context> Pool = new DefaultObjectPool<Context>(
                new DefaultPooledObjectPolicy<Context>(), maximumRetained: 16);

            /// <summary>
            /// Rents a Context from the pool and initializes it.
            /// Call Dispose() when done to return to pool.
            /// </summary>
            public static Context Rent(MessageDefinition message, Tags tags, int last)
            {
                var instance = Pool.Get();
                instance.Initialize(message, tags, last);
                return instance;
            }

            /// <summary>
            /// Default constructor for object pool.
            /// </summary>
            public Context()
            {
                // Collections created with default capacity
                // They will grow as needed and retain capacity across reuses
            }

            private void Initialize(MessageDefinition message, Tags tags, int last)
            {
                MsgType = message.MsgType;
                MsgDefinition = message;
                Tags = tags;
                Last = last;
                CurrentTagPosition = 0;
                _disposed = false;
            }

            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;

                // Clear collections (keeps capacity for reuse)
                Segments.Clear();
                StructureStack.Clear();

                // Clear references
                MsgType = null!;
                MsgDefinition = null;
                Tags = null!;

                // Return to pool
                Pool.Return(this);
            }

            public string MsgType { get; private set; } = null!;
            public List<SegmentDescription> Segments { get; } = new();
            public Stack<SegmentDescription> StructureStack { get; } = new(8);
            public int CurrentTagPosition { get; set; }
            public Tags Tags { get; private set; } = null!;
            public int Last { get; private set; }
            public MessageDefinition? MsgDefinition { get; private set; }
            public SegmentDescription? Peek => StructureStack.Count > 0 ? StructureStack.Peek() : null;

            private bool _disposed;
        }
    }
}
