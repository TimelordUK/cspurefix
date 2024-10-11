using PureFix.Transport.Session;
using PureFix.Transport.Store;

namespace PureFix.Test.Env.Experiment
{
    public interface IFixLogRecovery
    {
        string LogFilePath { get; set; }
        int? MySeqNum { get; }
        int? PeerSeqNum { get; }
        Task<FixMsgStoreState> AddRecord(IFixMsgStoreRecord record);
        Task Recover();
    }
}