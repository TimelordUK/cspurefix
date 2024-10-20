using PureFix.Types;

namespace PureFix.Transport
{
    public interface IFixLogParser
    {
        Action<IMessageView> OnView { get; set; }
        void Parse(string txt);
        void Snapshot(string file);
        void Tail(string file);
    }
}