using PureFix.Transport.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env.Experiment
{
    internal class TestLogRecovery : IFixLogRecovery
    {
        public int? MySeqNum => 0;

        public int? PeerSeqNum => 0;

        public Task<FixMsgStoreState> AddRecord(IFixMsgStoreRecord record)
        {
            return Task.FromResult(new FixMsgStoreState());
        }

        public Task Recover()
        {
            return Task.CompletedTask;
        }
    }
}
