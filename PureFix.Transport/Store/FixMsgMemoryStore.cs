using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public class FixMsgMemoryStore : IFixMsgStore
    {
        private readonly SortedList<int, IFixMsgStoreRecord> _sortedBySeqNum = [];
        private readonly HashSet<string> _sessionMessages = [
            MsgType.Logon, 
            MsgType.Logout, 
            MsgType.ResendRequest, 
            MsgType.Heartbeat, 
            MsgType.TestRequest, 
            MsgType.SequenceReset 
        ];

        private static int _nextId;
        private int _id = Interlocked.Increment(ref _nextId);
        
        public Task<FixMsgStoreState> Clear()
        {
            _sortedBySeqNum.Clear();
            return Task.FromResult(BuildState());
        }

        public Task<bool> Exists(int seq)
        {
            var record = GetRecord(seq);
            return Task.FromResult(record != null);
        }

        public Task<IFixMsgStoreRecord?> Get(int seq)
        {
            return Task.FromResult(GetRecord(seq));
        }

        private IFixMsgStoreRecord? GetRecord(int seq)
        {
            if (_sortedBySeqNum.TryGetValue(seq, out IFixMsgStoreRecord? record))
            {
                return record.Clone();
            }
            return null;
        }

        public Task<IFixMsgStoreRecord?[]> GetSeqNumRange(int from, int? to = null)
        {
            var floor = _sortedBySeqNum.Keys.BinarySearchIndexOf(from);
            to ??= _sortedBySeqNum.Keys.LastOrDefault();
            if (to == 0 || to == null) return Task.FromResult(new IFixMsgStoreRecord?[] { });
            to = Math.Abs(to.Value);
            int ceiling = to.Value;
            if (to != null)
            {
                ceiling = _sortedBySeqNum.Keys.BinarySearchIndexOf(to.Value);
            }
            ceiling = Math.Abs(ceiling);
            var records = Enumerable.Range(floor, ceiling).Select(GetRecord).Where(r => r != null).ToArray();
            return Task.FromResult(records);
        }

        public Task<FixMsgStoreState> GetState()
        {
            return Task.FromResult(BuildState());
        }

        public Task<FixMsgStoreState> Put(IFixMsgStoreRecord record)
        {
            if (_sessionMessages.Contains(record.MsgType)) return Task.FromResult(BuildState());
           _sortedBySeqNum.Add(record.SeqNum, record);
            return Task.FromResult(BuildState());
        }

        private FixMsgStoreState BuildState()
        {
            var keys = _sortedBySeqNum.Keys;
            if (keys.Count == 0) return new FixMsgStoreState();
            var low = keys.First();
            var last = keys.Last();
            return new FixMsgStoreState(_sortedBySeqNum.Keys.Count, low, last, _id);
        }
    }
}
