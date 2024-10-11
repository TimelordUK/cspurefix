using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Transport;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env.Experiment
{
    public class FixLogRecovery : IFixLogRecovery
    {
        public string LogFilePath { get; set; } = "logs";
        public string Filter { get; private set; }
        public IFixMsgStore FixMsgStore { get; private set; }
        public IFixMsgStoreRecord LastRecord { get; private set; }
        public IFixConfig Config { get; private set; }
        protected readonly ILogger m_logger;
        public int? MySeqNum { get; private set; } = 0;
        public int? PeerSeqNum { get; private set; } = 0;
        private IFixLogParser Parser { get; set; }

        public FixLogRecovery(IFixLogParser parser, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore)
        {
            Config = config;
            FixMsgStore = msgStore;
            Filter = config.Description.SenderCompID;
            m_logger = logFactory.MakeLogger(nameof(FixLogRecovery));
            Parser = parser;
        }

        private List<AsciiView> GetMessages(FileInfo fixLog, string filterComp)
        {
            var messages = new List<AsciiView>();
            Parser.OnView = v => messages.Add((AsciiView)v); 
            m_logger?.Info($"loading {fixLog.FullName} to recover store.");
            Parser.Snapshot(fixLog.FullName);
            var myMessages = messages.Where(v => v.GetString((int)MsgTag.SenderCompID) == filterComp).ToList();
            m_logger?.Info($"{fixLog.FullName} comp {filterComp} recovers {myMessages.Count}");
            return myMessages;
        }

        private FileInfo GetFixFileInfo()
        {
            var dir = new DirectoryInfo(LogFilePath);
            var fixLog = dir.EnumerateFiles()
                .OrderByDescending(x => x.CreationTime)
                .Where(f => f.Name.StartsWith($"{Config.Name()}-fix"))
                .LastOrDefault();
            return fixLog;
        }

        private int GetStartIndex(List<AsciiView> messages)
        {
            var starts = messages.FindLastIndex(v => v.GetInt32((int)MsgTag.MsgSeqNum) == 1);
            if (starts == -1)
            {
                m_logger?.Info("log has no MsgSeqNum 1 to start from");
                return -1;
            }
            return starts;
        }

        private async Task LoadStore(List<AsciiView> myMessages)
        {
            var starts = GetStartIndex(myMessages);
            if (starts == -1)
            {
                return;
            }

            var ends = myMessages.Count;
            for (var i = starts; i < ends; ++i)
            {
                var msg = myMessages[i];
                var record = FixMsgStoreRecord.ToMsgStoreRecord(msg);
                MySeqNum = record.SeqNum;
                if (FixMsgStore != null)
                {
                    await AddRecord(record);
                }
                LastRecord = record;
            }
        }

        private void RecoverPeer(List<AsciiView> peerMessages)
        {
            var starts = GetStartIndex(peerMessages);
            if (starts == -1)
            {
                return;
            }
            var ends = peerMessages.Count;
            for (var i = starts; i < ends; ++i)
            {
                var msg = peerMessages[i];
                PeerSeqNum = msg.GetInt32((int)MsgTag.MsgSeqNum);
            }
        }

        public async Task Recover()
        {
            if (Config.ResetSeqNumFlag())
            {
                m_logger?.Info("ResetSeqNumFlag hence no recovery from fix log.");
                return;
            }

            try
            {
                var dir = new DirectoryInfo("logs");
                var fixLog = GetFixFileInfo();
                if (fixLog == null)
                {
                    m_logger?.Info("no fix log to recover from.");
                    return;
                }
                var senderComp = Config.Description.SenderCompID;
                var targetComp = Config.Description.TargetCompID;
                var myMessages = GetMessages(fixLog, senderComp);
                var peerMessages = GetMessages(fixLog, targetComp);

                await LoadStore(myMessages);
                RecoverPeer(peerMessages);
                m_logger?.Info($"Recover MySeqNum = {MySeqNum} PeerSeqNum = {PeerSeqNum}");
            }
            catch (Exception ex)
            {
                m_logger?.Error(ex);
            }
        }

        public async Task<FixMsgStoreState> AddRecord(IFixMsgStoreRecord record)
        {
            var state = await FixMsgStore.Put(record);
            m_logger.Info($"[{record.MsgType},{record.SeqNum}] store state {state}");
            return state;
        }
    }
}
