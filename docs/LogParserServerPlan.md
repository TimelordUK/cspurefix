# FIX Log Parser Server - Implementation Plan

## Vision
A containerized server that accepts FIX log file uploads from a React GUI, auto-detects the appropriate dictionary via tag 49 (SenderCompID), and returns tokenized messages for display.

## Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                     React GUI (Future)                           │
│  - Upload log file                                               │
│  - Select from registry dropdown (optional)                      │
│  - View tokenized messages in grid                               │
│  - Click message -> expand to JSON                               │
└──────────────────────────┬──────────────────────────────────────┘
                           │ HTTP POST /api/parse/upload
                           ▼
┌─────────────────────────────────────────────────────────────────┐
│                   SeeFixServer (Docker)                          │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ LogParserController                                       │    │
│  │   POST /api/parse/upload - file upload + auto-detect      │    │
│  │   POST /api/parse/tokenize - buffer + dictName            │    │
│  │   GET  /api/registry - list all entries                   │    │
│  └─────────────────────────────────────────────────────────┘    │
│                           │                                      │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ TypeRegistry (Enhanced DictContainer)                     │    │
│  │   - FindBySenderCompId(tag49) -> DictMeta                 │    │
│  │   - GetParser(name) -> IDictMessageParser                 │    │
│  │   - Each entry has: XML path, assembly, senderCompIds[]   │    │
│  └─────────────────────────────────────────────────────────┘    │
│                           │                                      │
│  ┌────────────┬────────────┬────────────┬────────────┐         │
│  │ FIX44      │ FIX50SP2   │ JPMFX      │ BBGFX      │         │
│  │ Parser     │ Parser     │ Parser     │ Parser     │         │
│  └────────────┴────────────┴────────────┴────────────┘         │
└─────────────────────────────────────────────────────────────────┘
```

## Phase 1: Enhance Registry with SenderCompID Mapping

**Goal**: Add tag 49 -> dictionary lookup

### Step 1.1: Update DictMeta to support multiple SenderCompIds

**File**: `PureFix.LogMessageParser/DictMeta.cs`

```csharp
public class DictMeta {
    public string? Name { get; set; }
    public string? Dict { get; set; }
    public string? Type { get; set; }
    public string? Sender { get; set; }  // Already exists!

    // NEW: Support multiple senders for matching
    public List<string>? SenderCompIds { get; set; }

    // NEW: Display name for UI
    public string? DisplayName { get; set; }

    // NEW: Is this the default fallback?
    public bool IsDefault { get; set; }
}
```

### Step 1.2: Add FindBySenderCompId to DictContainer

**File**: `PureFix.LogMessageParser/IDictContainer.cs`

```csharp
public interface IDictContainer
{
    IDictMessageParser? this[string name] { get; }
    IReadOnlyDictionary<string, IDictMessageParser> Parsers { get; }
    void Init(string jsonMetaDesFile, Assembly typesAssembly);

    // NEW
    IDictMessageParser? FindBySenderCompId(string senderCompId);
    DictMeta? GetDefaultMeta();
    IEnumerable<DictMeta> GetAllMetas();
}
```

### Step 1.3: Update DictionaryMeta.json

```json
{
  "metas": [
    {
      "name": "FIX50SP2",
      "displayName": "Standard FIX 5.0 SP2",
      "dict": "Data/FIX50SP2.xml",
      "type": "PureFix.Types.FIX50SP2",
      "senderCompIds": ["init-comp", "accept-comp"],
      "isDefault": true
    },
    {
      "name": "JPMFX44",
      "displayName": "JPMorgan FIX 4.4",
      "dict": "Dictionaries/JPMFX44.xml",
      "type": "PureFix.Types.JPMFX",
      "senderCompIds": ["JPMORGAN", "JPM-FX", "JPMFX"]
    }
  ]
}
```

---

## Phase 2: Add File Upload Endpoint

**Goal**: React can upload a .log file

### Step 2.1: Create LogParserController

**File**: `SeeFixServer/Controllers/LogParserController.cs`

```csharp
[ApiController]
[Route("api/[controller]")]
public class LogParserController : ControllerBase
{
    private readonly IDictContainer _registry;

    public LogParserController(IDictContainer registry)
    {
        _registry = registry;
    }

    /// <summary>
    /// Upload a FIX log file and auto-detect dictionary by tag 49
    /// </summary>
    [HttpPost("upload")]
    public async Task<ActionResult<ParseUploadResult>> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file provided");

        using var reader = new StreamReader(file.OpenReadStream());
        var content = await reader.ReadToEndAsync();
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length == 0)
            return BadRequest("Empty file");

        // Extract tag 49 from first message
        var senderCompId = ExtractTag49(lines[0]);

        // Find matching parser
        var parser = senderCompId != null
            ? _registry.FindBySenderCompId(senderCompId)
            : null;

        parser ??= _registry[_registry.GetDefaultMeta()?.Name ?? ""];

        if (parser == null)
            return BadRequest("No matching dictionary found");

        // Parse all messages
        var request = new ParseRequest
        {
            DictName = parser.Meta.Name,
            Messages = lines.ToList(),
            Delim = (byte)'|'
        };

        var (result, _) = parser.Parse(request);

        return Ok(new ParseUploadResult
        {
            DetectedSenderCompId = senderCompId,
            UsedDictionary = parser.Meta.Name,
            MessageCount = result.Messages.Count,
            Messages = result.Messages
        });
    }

    /// <summary>
    /// Get all registered dictionaries for UI dropdown
    /// </summary>
    [HttpGet("registry")]
    public ActionResult<IEnumerable<RegistryEntry>> GetRegistry()
    {
        return Ok(_registry.GetAllMetas().Select(m => new RegistryEntry
        {
            Name = m.Name,
            DisplayName = m.DisplayName ?? m.Name,
            SenderCompIds = m.SenderCompIds ?? new List<string>(),
            IsDefault = m.IsDefault
        }));
    }

    private static string? ExtractTag49(string message)
    {
        // Find 49=value pattern
        var match = Regex.Match(message, @"\|49=([^|]+)\|");
        if (match.Success)
            return match.Groups[1].Value;

        // Try SOH delimiter
        match = Regex.Match(message, @"\x0149=([^\x01]+)\x01");
        return match.Success ? match.Groups[1].Value : null;
    }
}

public class ParseUploadResult
{
    public string? DetectedSenderCompId { get; set; }
    public string? UsedDictionary { get; set; }
    public int MessageCount { get; set; }
    public List<ParsedMessage> Messages { get; set; } = new();
}

public class RegistryEntry
{
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public List<string> SenderCompIds { get; set; } = new();
    public bool IsDefault { get; set; }
}
```

---

## Phase 3: Tokenized Response (Lightweight)

**Goal**: Return only tag info without full JSON conversion for initial display

### Step 3.1: Add TokenizeOnly mode

**File**: `PureFix.LogMessageParser/ParseRequest.cs`

```csharp
public class ParseRequest
{
    public string? DictName { get; set; }
    public List<string>? Messages { get; set; }
    public byte Delim { get; set; } = AsciiChars.Pipe;

    // NEW: Skip JSON conversion, only return tags
    public bool TokenizeOnly { get; set; } = false;
}
```

### Step 3.2: Optimize Parse for TokenizeOnly

When `TokenizeOnly = true`:
- Skip `MessageFactory.ToFixMessage()` call
- Only populate `ParsedMessage.Tags` list
- Much faster for large files

---

## Phase 4: Docker Containerization

**Goal**: Package server as Docker container

### Step 4.1: Create Dockerfile

**File**: `SeeFixServer/Dockerfile`

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SeeFixServer/SeeFixServer.csproj"
RUN dotnet build "SeeFixServer/SeeFixServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SeeFixServer/SeeFixServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy dictionary files
COPY Data/*.xml ./Data/
COPY SeeFixServer/DictionaryMeta.json .

ENTRYPOINT ["dotnet", "SeeFixServer.dll"]
```

### Step 4.2: Create docker-compose.yml

```yaml
version: '3.8'
services:
  seefix-server:
    build:
      context: .
      dockerfile: SeeFixServer/Dockerfile
    ports:
      - "5000:5000"
    volumes:
      - ./Dictionaries:/app/Dictionaries:ro
      - ./DictionaryMeta.json:/app/DictionaryMeta.json:ro
    environment:
      - ASPNETCORE_URLS=http://+:5000
```

---

## Phase 5: React GUI Integration (Future)

### API Contract Summary

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/logparser/registry` | GET | List all dictionaries for dropdown |
| `/api/logparser/upload` | POST (multipart) | Upload file, auto-detect, return tokenized |
| `/api/logparser/parse` | POST | Parse with explicit dictionary |
| `/api/logparser/message/{offset}` | GET | Get full JSON for single message |

### Response Formats

**GET /api/logparser/registry**
```json
[
  { "name": "FIX50SP2", "displayName": "Standard FIX 5.0 SP2", "senderCompIds": ["init-comp"], "isDefault": true },
  { "name": "JPMFX44", "displayName": "JPMorgan FIX 4.4", "senderCompIds": ["JPMORGAN"] }
]
```

**POST /api/logparser/upload**
```json
{
  "detectedSenderCompId": "init-comp",
  "usedDictionary": "FIX50SP2",
  "messageCount": 100,
  "messages": [
    {
      "name": "Logon",
      "tags": [
        { "fid": 35, "name": "MsgType", "value": "A", "description": "Logon" },
        { "fid": 49, "name": "SenderCompID", "value": "init-comp" },
        { "fid": 56, "name": "TargetCompID", "value": "accept-comp" }
      ]
    }
  ]
}
```

---

## Implementation Order

| Step | Description | Est. Effort |
|------|-------------|-------------|
| 1 | Enhance DictMeta with SenderCompIds | 30 min |
| 2 | Add FindBySenderCompId to DictContainer | 30 min |
| 3 | Create LogParserController with upload | 1 hour |
| 4 | Add TokenizeOnly mode | 30 min |
| 5 | Create Dockerfile | 30 min |
| 6 | Test with sample log file | 30 min |
| **Total** | | **~4 hours** |

---

## Current Files to Modify

1. `PureFix.LogMessageParser/DictMeta.cs` - Add fields
2. `PureFix.LogMessageParser/IDictContainer.cs` - Add methods
3. `PureFix.LogMessageParser/DictContainer.cs` - Implement lookup
4. `SeeFixServer/Controllers/LogParserController.cs` - New file
5. `SeeFixServer/DictionaryMeta.json` - Add senderCompIds

## Future Enhancements

- [ ] WebSocket streaming for large files
- [ ] Message filtering by MsgType regex
- [ ] SQL-CLI field extraction mode
- [ ] Message store integration for session replay
- [ ] Health check endpoint for container orchestration
