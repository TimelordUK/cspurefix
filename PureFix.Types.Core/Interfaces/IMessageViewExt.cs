
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport
{
    /// <summary>
    /// Deferred string wrapper for lazy logging. Avoids string allocation
    /// when the log level is disabled. Only allocates when ToString() is
    /// called by the logger during actual log output.
    /// </summary>
    public readonly struct LazyTag
    {
        private readonly IMessageView _view;
        private readonly int _tag;

        public LazyTag(IMessageView view, int tag)
        {
            _view = view;
            _tag = tag;
        }

        public override string ToString() => _view.GetString(_tag) ?? string.Empty;
    }

    public static class IMessageViewExt
    {
        /// <summary>
        /// Returns a lazy wrapper that defers string allocation until ToString() is called.
        /// Use with structured logging to avoid allocations when log level is disabled.
        /// </summary>
        /// <example>
        /// logger?.Info("text = {Text}", view.Lazy((int)MsgTag.Text));
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LazyTag Lazy(this IMessageView view, int tag) => new(view, tag);

        public static int? HeartBtInt(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.HeartBtInt);
        }

        public static string? SenderCompID(this IMessageView view)
        {
            return view.GetString((int)MsgTag.SenderCompID);
        }

        public static string? Username(this IMessageView view)
        {
            return view.GetString((int)MsgTag.Username);
        }

        public static string? Password(this IMessageView view)
        {
            return view.GetString((int)MsgTag.Password);
        }

        public static int? MsgSeqNum(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.MsgSeqNum);
        }

        public static int? BeginSeqNo(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.BeginSeqNo);
        }

        public static int? EndSeqNo(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.EndSeqNo);
        }

        public static string? MsgType(this IMessageView view)
        {
            return view.GetString((int)MsgTag.MsgType);
        }

        public static DateTime? SendingTime(this IMessageView view)
        {
            return view.GetDateTime((int)MsgTag.SendingTime);
        }

        public static bool? ResetSeqNumFlag(this IMessageView view)
        {
            return view.GetBool((int)MsgTag.ResetSeqNumFlag);
        }
    }
}
