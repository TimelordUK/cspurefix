using Arrow.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Transport.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PureFix.Transport;


namespace PureFix.Test.Env.Experiment
{
    internal class FixLogRecovery
    {
        public string LogFilePath { get; private set; }
        public string Filter { get; private set; }
        public IFixMsgStore FixMsgStore { get; private set; }
        public IFixMsgStoreRecord LastRecord { get; private set; }
        public IFixConfig Config { get; private set; }
        protected readonly ILogger m_logger;
        public int? MySeqNum { get; private set; } = 1;
        public int? PeerSeqNum { get; private set; } = 1;

        public FixLogRecovery(string logPath, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore = null)
        {
            Config = config;
            LogFilePath = logPath;
            FixMsgStore = msgStore;
            Filter = config.Description.SenderCompID;
            m_logger = logFactory.MakeLogger(nameof(FixLogRecovery));
        }

        private List<AsciiView> GetMessages(FileInfo fixLog, string filterComp)
        {
            var messages = new List<AsciiView>();
            var fixLogger = new FixLogParser(Config)
            {
                OnView = v => messages.Add((AsciiView)v)
            };
            m_logger?.Info($"loading {fixLog.FullName} to recover store.");
            fixLogger.Snapshot(fixLog.FullName);
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

    internal abstract class BaseApp : AsciiSession
    {
        protected readonly ILogger m_logger;
        protected readonly ILogger m_fixLog;
        public (ILogger appLog, ILogger fixLog) Logs => (m_logger, m_fixLog);
        public string FixLogName { get; private set; }
        public string AppLogName { get; private set; }
        private FixLogRecovery Recovery { get; set; }

        protected BaseApp(IFixConfig config, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore msgStore, AsyncWorkQueue q, IFixClock clock) : base(config, logFactory, fixMessageFactory, parser, encoder, msgStore, q, clock)
        {
            m_logReceivedMessages = true;
            var me = config.Name();
            FixLogName = $"csfix.{me}.fix";
            AppLogName = $"csfix.{me}.app";
            m_fixLog = logFactory.MakePlainLogger(FixLogName);
            m_logger = logFactory.MakeLogger(AppLogName);
            Recovery = new FixLogRecovery("logs", logFactory, config, msgStore);
        }

        protected override void OnDecoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
        }

        // have not yet logged in so here we can recover seq numbers if possible.
        protected override async Task OnRun()
        {
            await Recovery.Recover();
        }

        protected override async Task OnEncoded(string msgType, int seqNum, string txt)
        {
            var record = new FixMsgStoreRecord(msgType, m_clock.Current, seqNum, txt);
            await Recovery.AddRecord(record);
            m_fixLog.Info(txt);
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
