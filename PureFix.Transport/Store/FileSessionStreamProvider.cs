using System.Text;

namespace PureFix.Transport.Store;

/// <summary>
/// File-based implementation of ISessionStreamProvider.
/// Creates files in the specified directory with QuickFix-compatible naming.
/// </summary>
public sealed class FileSessionStreamProvider : ISessionStreamProvider, IAsyncDisposable
{
    private readonly SessionId _sessionId;
    private readonly string _directory;
    private FileStream? _bodyStream;
    private StreamWriter? _headerWriter;

    public FileSessionStreamProvider(SessionId sessionId, string directory)
    {
        _sessionId = sessionId;
        _directory = directory;
    }

    private string GetFilePath(string extension) => _sessionId.GetFilePath(_directory, extension);

    public Stream OpenBodyStream()
    {
        if (_bodyStream != null)
            return _bodyStream;

        Directory.CreateDirectory(_directory);
        _bodyStream = new FileStream(
            GetFilePath("body"),
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            // ReadWrite rather than Read: when a counterparty reconnects, the replacement
            // session opens these files while the displaced session may not have released
            // its handles yet. With FileShare.Read that handoff throws and the reconnecting
            // client cannot log on at all. Only one session per SessionId is ever live -
            // the registry guarantees it - so concurrent writers are not a concern.
            FileShare.ReadWrite);
        _bodyStream.Seek(0, SeekOrigin.End); // Append mode
        return _bodyStream;
    }

    public StreamWriter OpenHeaderWriter()
    {
        if (_headerWriter != null)
            return _headerWriter;

        Directory.CreateDirectory(_directory);
        var headerStream = new FileStream(
            GetFilePath("header"),
            FileMode.OpenOrCreate,
            FileAccess.Write,
            FileShare.ReadWrite); // see OpenBodyStream
        headerStream.Seek(0, SeekOrigin.End); // Append mode
        // Use UTF8 without BOM for QuickFix compatibility
        _headerWriter = new StreamWriter(headerStream, new UTF8Encoding(false)) { AutoFlush = false };
        return _headerWriter;
    }

    public Task<string?> ReadSeqNumsAsync() => ReadAllTextShared(GetFilePath("seqnums"));

    public Task WriteSeqNumsAsync(string content) => WriteAllTextShared(GetFilePath("seqnums"), content);

    public Task<string?> ReadSessionTimeAsync() => ReadAllTextShared(GetFilePath("session"));

    public Task WriteSessionTimeAsync(string content) => WriteAllTextShared(GetFilePath("session"), content);

    public async Task<string[]> ReadHeaderLinesAsync()
    {
        var content = await ReadAllTextShared(GetFilePath("header"));
        return content?.Split('\n').Select(l => l.TrimEnd('\r')).ToArray() ?? [];
    }

    // File.ReadAllTextAsync and friends open with FileShare.Read, which collides with the
    // body/header writers this provider keeps open (and with a displaced session's handles
    // during a reconnect). Every access goes through these helpers so the whole store agrees
    // on FileShare.ReadWrite.

    private static async Task<string?> ReadAllTextShared(string path)
    {
        if (!File.Exists(path)) return null;

        await using var stream = new FileStream(
            path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    private async Task WriteAllTextShared(string path, string content)
    {
        Directory.CreateDirectory(_directory);

        await using var stream = new FileStream(
            path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        await using var writer = new StreamWriter(stream, new UTF8Encoding(false));
        await writer.WriteAsync(content);
    }

    public async Task ResetAsync()
    {
        // Close existing streams
        if (_headerWriter != null)
        {
            await _headerWriter.DisposeAsync();
            _headerWriter = null;
        }
        if (_bodyStream != null)
        {
            await _bodyStream.DisposeAsync();
            _bodyStream = null;
        }

        // Delete existing files
        DeleteFileIfExists("seqnums");
        DeleteFileIfExists("session");
        DeleteFileIfExists("header");
        DeleteFileIfExists("body");
    }

    public Stream? GetBodyStream() => _bodyStream;

    private void DeleteFileIfExists(string extension)
    {
        var path = GetFilePath(extension);
        if (File.Exists(path))
            File.Delete(path);
    }

    public async ValueTask DisposeAsync()
    {
        if (_headerWriter != null)
        {
            await _headerWriter.DisposeAsync();
            _headerWriter = null;
        }
        if (_bodyStream != null)
        {
            await _bodyStream.DisposeAsync();
            _bodyStream = null;
        }
    }
}
