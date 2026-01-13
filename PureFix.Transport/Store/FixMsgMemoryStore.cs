using PureFix.Types;
using PureFix.Types.Core;
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

        public FixMsgMemoryStore(string name)
        {
            Name = name;
        }

        private static int _nextId;
        private int _id = Interlocked.Increment(ref _nextId);
        public string Name { get; }


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
            if (_sortedBySeqNum.TryGetValue(seq, out var record))
            {
                return record.Clone();
            }
            return null;
        }

        private IFixMsgStoreRecord? GetNearestRecord(int seq)
        {
            seq = Math.Max(seq, 0);
            IFixMsgStoreRecord? startRecord = null;
            if (_sortedBySeqNum.TryGetValue(seq, out var record))
            {
                startRecord = record;
            }
            else
            {
                var nearestKey = Math.Abs(_sortedBySeqNum.Keys.BinarySearchIndexOf(seq));
                if (_sortedBySeqNum.TryGetValue(nearestKey, out var starting))
                {
                    startRecord = starting;
                }
            }
            return startRecord;
        }

        public Task<IFixMsgStoreRecord?[]> GetSeqNumRange(int from, int? to = null)
        {
            if (_sessionMessages.Count == 0)
            {
                return Task.FromResult(Array.Empty<IFixMsgStoreRecord?>());
            }
            var lastkv = _sortedBySeqNum.LastOrDefault();
            var lastRecord = lastkv.Value;
            from = Math.Min(lastkv.Key, from);
            var startRecord = GetNearestRecord(from);
            if (to != null)
            {
                to = Math.Max(from, to.Value);
                var stored = GetNearestRecord(to.Value);
                if (stored != null)
                {
                    lastRecord = stored;
                }
            }
            startRecord ??= _sortedBySeqNum.FirstOrDefault().Value;
            var records = Enumerable.Range(startRecord.SeqNum, lastRecord.SeqNum - startRecord.SeqNum + 1).Select(GetRecord).Where(r => r != null).ToArray();
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
            return new FixMsgStoreState(_sortedBySeqNum.Keys.Count, low, last, Name);
        }
    }
}
