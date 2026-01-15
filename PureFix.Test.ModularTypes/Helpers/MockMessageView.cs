using PureFix.Types;

namespace PureFix.Test.ModularTypes.Helpers;

/// <summary>
/// Mock implementation of IMessageView for unit testing.
/// Uses dictionaries to store tag values, allowing tests to construct
/// specific message scenarios without parsing actual FIX messages.
/// </summary>
internal class MockMessageView : IMessageView
{
    private readonly Dictionary<int, string> _stringValues = new();
    private readonly Dictionary<int, int> _intValues = new();
    private readonly Dictionary<int, bool> _boolValues = new();
    private readonly Dictionary<int, double> _doubleValues = new();
    private readonly Dictionary<int, decimal> _decimalValues = new();
    private readonly Dictionary<int, DateTime> _dateTimeValues = new();
    private readonly Dictionary<int, byte[]> _byteArrayValues = new();

    public MockMessageView() { }

    /// <summary>
    /// Creates a mock message view with the specified message type and sequence number.
    /// </summary>
    public static MockMessageView Create(string msgType, int seqNum)
    {
        var view = new MockMessageView();
        view.SetString((int)MsgTag.MsgType, msgType);
        view.SetInt((int)MsgTag.MsgSeqNum, seqNum);
        return view;
    }

    /// <summary>
    /// Creates a mock Logon message.
    /// </summary>
    public static MockMessageView Logon(int seqNum, bool? resetSeqNumFlag = null, int heartBtInt = 30)
    {
        var view = Create(MsgType.Logon, seqNum);
        view.SetInt((int)MsgTag.HeartBtInt, heartBtInt);
        if (resetSeqNumFlag.HasValue)
            view.SetBool((int)MsgTag.ResetSeqNumFlag, resetSeqNumFlag.Value);
        return view;
    }

    /// <summary>
    /// Creates a mock ResendRequest message.
    /// </summary>
    public static MockMessageView ResendRequest(int seqNum, int beginSeqNo, int endSeqNo)
    {
        var view = Create(MsgType.ResendRequest, seqNum);
        view.SetInt((int)MsgTag.BeginSeqNo, beginSeqNo);
        view.SetInt((int)MsgTag.EndSeqNo, endSeqNo);
        return view;
    }

    /// <summary>
    /// Creates a mock SequenceReset message.
    /// </summary>
    public static MockMessageView SequenceReset(int seqNum, int newSeqNo, bool gapFillFlag = true)
    {
        var view = Create(MsgType.SequenceReset, seqNum);
        view.SetInt((int)MsgTag.NewSeqNo, newSeqNo);
        view.SetBool((int)MsgTag.GapFillFlag, gapFillFlag);
        return view;
    }

    /// <summary>
    /// Creates a mock Reject message.
    /// </summary>
    public static MockMessageView Reject(int seqNum, int refSeqNum, string refMsgType, string? text = null)
    {
        var view = Create(MsgType.Reject, seqNum);
        view.SetInt((int)MsgTag.RefSeqNum, refSeqNum);
        view.SetString((int)MsgTag.RefMsgType, refMsgType);
        if (text != null)
            view.SetString((int)MsgTag.Text, text);
        return view;
    }

    /// <summary>
    /// Creates a mock Heartbeat message.
    /// </summary>
    public static MockMessageView Heartbeat(int seqNum, string? testReqId = null)
    {
        var view = Create(MsgType.Heartbeat, seqNum);
        if (testReqId != null)
            view.SetString((int)MsgTag.TestReqID, testReqId);
        return view;
    }

    /// <summary>
    /// Creates a mock TestRequest message.
    /// </summary>
    public static MockMessageView TestRequest(int seqNum, string testReqId)
    {
        var view = Create(MsgType.TestRequest, seqNum);
        view.SetString((int)MsgTag.TestReqID, testReqId);
        return view;
    }

    /// <summary>
    /// Creates a mock application message (e.g., NewOrderSingle "D").
    /// </summary>
    public static MockMessageView ApplicationMessage(string msgType, int seqNum, bool? possDupFlag = null)
    {
        var view = Create(msgType, seqNum);
        if (possDupFlag.HasValue)
            view.SetBool((int)MsgTag.PossDupFlag, possDupFlag.Value);
        return view;
    }

    #region Setters

    public MockMessageView SetString(int tag, string value)
    {
        _stringValues[tag] = value;
        return this;
    }

    public MockMessageView SetInt(int tag, int value)
    {
        _intValues[tag] = value;
        return this;
    }

    public MockMessageView SetBool(int tag, bool value)
    {
        _boolValues[tag] = value;
        return this;
    }

    public MockMessageView SetDouble(int tag, double value)
    {
        _doubleValues[tag] = value;
        return this;
    }

    public MockMessageView SetDecimal(int tag, decimal value)
    {
        _decimalValues[tag] = value;
        return this;
    }

    public MockMessageView SetDateTime(int tag, DateTime value)
    {
        _dateTimeValues[tag] = value;
        return this;
    }

    public MockMessageView SetByteArray(int tag, byte[] value)
    {
        _byteArrayValues[tag] = value;
        return this;
    }

    #endregion

    #region IMessageView Implementation

    public string? GetString(int tag) => _stringValues.TryGetValue(tag, out var v) ? v : null;
    public string? GetString(string name) => null; // Not supported in mock

    public int? GetInt32(int tag) => _intValues.TryGetValue(tag, out var v) ? v : null;
    public int? GetInt32(string name) => null;

    public bool? GetBool(int tag) => _boolValues.TryGetValue(tag, out var v) ? v : null;
    public bool? GetBool(string name) => null;

    public double? GetDouble(int tag) => _doubleValues.TryGetValue(tag, out var v) ? v : null;
    public double? GetDouble(string name) => null;

    public decimal? GetDecimal(int tag) => _decimalValues.TryGetValue(tag, out var v) ? v : null;
    public decimal? GetDecimal(string name) => null;

    public DateTime? GetDateTime(int tag) => _dateTimeValues.TryGetValue(tag, out var v) ? v : null;

    public TimeOnly? GetTimeOnly(int tag) => null;
    public DateOnly? GetDateOnly(int tag) => null;

    public byte[]? GetByteArray(int tag) => _byteArrayValues.TryGetValue(tag, out var v) ? v : null;
    public byte[]? GetByteArray(string name) => null;

    public Memory<byte>? GetMemory(int tag) => _byteArrayValues.TryGetValue(tag, out var v) ? v : null;
    public MonthYear? GetMonthYear(int tag) => null;

    public int GroupCount() => 0;
    public IMessageView? GetGroupInstance(int i) => null;
    public IMessageView? GetView(string name) => null;

    public string BufferString() => $"MockMessageView[MsgType={GetString((int)MsgTag.MsgType)}, SeqNum={GetInt32((int)MsgTag.MsgSeqNum)}]";
    public int Checksum() => 0;

    // Span-based API - not fully supported in mock
    public ReadOnlySpan<byte> GetSpan(int tag) => ReadOnlySpan<byte>.Empty;
    public bool TryGetSpan(int tag, out ReadOnlySpan<byte> value)
    {
        value = ReadOnlySpan<byte>.Empty;
        return false;
    }

    public bool IsTagEqual(int tag, ReadOnlySpan<byte> expected) => false;
    public bool TagStartsWith(int tag, ReadOnlySpan<byte> prefix) => false;
    public int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1) => -1;
    public int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2) => -1;
    public int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2, ReadOnlySpan<byte> value3) => -1;

    // Try-pattern getters
    public bool TryGetInt32(int tag, out int value)
    {
        if (_intValues.TryGetValue(tag, out value)) return true;
        value = 0;
        return false;
    }

    public bool TryGetInt64(int tag, out long value)
    {
        if (_intValues.TryGetValue(tag, out var intVal))
        {
            value = intVal;
            return true;
        }
        value = 0;
        return false;
    }

    public bool TryGetDouble(int tag, out double value)
    {
        if (_doubleValues.TryGetValue(tag, out value)) return true;
        value = 0;
        return false;
    }

    public bool TryGetDecimal(int tag, out decimal value)
    {
        if (_decimalValues.TryGetValue(tag, out value)) return true;
        value = 0;
        return false;
    }

    public bool TryGetBool(int tag, out bool value)
    {
        if (_boolValues.TryGetValue(tag, out value)) return true;
        value = false;
        return false;
    }

    #endregion

    public override string ToString() => BufferString();
}
