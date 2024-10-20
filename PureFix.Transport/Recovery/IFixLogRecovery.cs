using PureFix.Transport.Session;
using PureFix.Transport.Store;

namespace PureFix.Transport.Recovery
{
    public interface IFixLogRecovery
    {
        int? MySeqNum { get; }
        int? PeerSeqNum { get; }
        FixMsgStoreState? LastStoreState { get; }
        Task<FixMsgStoreState> AddRecord(IFixMsgStoreRecord record);
        Task Recover();
    }
}