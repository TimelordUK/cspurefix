using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Recovery
{
    public class FixLogRecovery : IFixLogRecovery
    {
        public string LogFilePath { get; set; } = "logs";
      
        public IFixMsgStore FixMsgStore { get; }
        public FixMsgStoreState? LastStoreState { get; private set; }
        public IFixConfig Config { get; }
        protected readonly ILogger? m_logger;
        public int? MySeqNum { get; private set; }
        public int? PeerSeqNum { get; private set; }
        private IFixLogParser Parser { get; }

        public FixLogRecovery(IFixLogParser parser, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore)
        {
            ArgumentNullException.ThrowIfNull(config?.Description);
            Config = config;
            FixMsgStore = msgStore;          
            m_logger = logFactory.MakeLogger(nameof(FixLogRecovery));
            Parser = parser;
        }

        private List<AsciiView> GetAllMessages(FileInfo fixLog)
        {
            var messages = new List<AsciiView>();
            try
            {
                Parser.OnView = v =>
                {                    
                    messages.Add((AsciiView)v);
                };
                m_logger?.Info($"loading {fixLog.FullName} to recover store.");
                Parser.Snapshot(fixLog.FullName);
            }
            catch (Exception ex)
            {
                m_logger?.Info($"error - parsed count: {messages.Count} (clearing)");
                m_logger?.Error(ex);
                messages.Clear();
            }
            return messages;
        }

        private List<AsciiView> GetMessages(List<AsciiView> messages, string filterComp)
        {
            var myMessages = messages.Where(v => v.GetString((int)MsgTag.SenderCompID) == filterComp).ToList();
            m_logger?.Info($"comp {filterComp} recovers {myMessages.Count}");
            return myMessages;
        }

        protected virtual FileInfo? GetFixFileInfo()
        {
            var filter = $"{Config.Name()}-fix";
            var dir = new DirectoryInfo(LogFilePath);
            if (!dir.Exists) return null;
            var fixLog = dir
                .EnumerateFiles()
                .OrderByDescending(x => x.CreationTime)
                .LastOrDefault(f => f.Name.StartsWith(filter));
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
                    LastStoreState = await AddRecord(record);
                }                
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

        public virtual async Task Recover()
        {
            ArgumentNullException.ThrowIfNull(Config?.Description?.SenderCompID);
            ArgumentNullException.ThrowIfNull(Config?.Description?.TargetCompID);

            if (Config.ResetSeqNumFlag())
            {
                m_logger?.Info("ResetSeqNumFlag hence no recovery from fix log.");
                return;
            }

            try
            {
                var fixLog = GetFixFileInfo();
                if (fixLog == null)
                {
                    m_logger?.Info("no fix log to recover from.");
                    return;
                }
                var senderComp = Config.Description.SenderCompID;
                var targetComp = Config.Description.TargetCompID;
                var all = GetAllMessages(fixLog);
                var myMessages = GetMessages(all, senderComp);
                var peerMessages = GetMessages(all, targetComp);

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
            m_logger?.Info($"[{record.MsgType},{record.SeqNum}] store state {state}");
            return state;
        }
    }
}
