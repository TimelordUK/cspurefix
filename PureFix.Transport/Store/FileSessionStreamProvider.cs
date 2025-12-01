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
            FileShare.Read);
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
            FileShare.Read);
        headerStream.Seek(0, SeekOrigin.End); // Append mode
        // Use UTF8 without BOM for QuickFix compatibility
        _headerWriter = new StreamWriter(headerStream, new UTF8Encoding(false)) { AutoFlush = false };
        return _headerWriter;
    }

    public async Task<string?> ReadSeqNumsAsync()
    {
        var path = GetFilePath("seqnums");
        if (!File.Exists(path))
            return null;
        return await File.ReadAllTextAsync(path);
    }

    public async Task WriteSeqNumsAsync(string content)
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(GetFilePath("seqnums"), content);
    }

    public async Task<string?> ReadSessionTimeAsync()
    {
        var path = GetFilePath("session");
        if (!File.Exists(path))
            return null;
        return await File.ReadAllTextAsync(path);
    }

    public async Task WriteSessionTimeAsync(string content)
    {
        Directory.CreateDirectory(_directory);
        await File.WriteAllTextAsync(GetFilePath("session"), content);
    }

    public async Task<string[]> ReadHeaderLinesAsync()
    {
        var path = GetFilePath("header");
        if (!File.Exists(path))
            return [];
        return await File.ReadAllLinesAsync(path);
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
