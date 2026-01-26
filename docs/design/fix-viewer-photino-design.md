# FIX Log Viewer - Photino.NET Design Document

## Overview

A standalone desktop application for viewing, filtering, and analyzing FIX protocol log files. Built with **Photino.NET + React**, with the cspurefix engine running **in-process** for maximum performance and simplicity.

**Primary Use Case:** Upload FIX log files from production systems, rapidly filter to find relevant messages, inspect tag details, and optionally forward messages to development endpoints for testing.

**Key Architectural Decision:** Using Photino.NET allows the cspurefix FIX parsing engine to run in-process with the desktop shell. No HTTP API, no serialization overhead, no separate server process. The React frontend communicates directly with .NET via message passing.

---

## Technology Stack

| Layer | Technology | Rationale |
|-------|------------|-----------|
| **Desktop Shell** | Photino.NET 4.0.16 | .NET native, in-process cspurefix, small bundle, system WebView |
| **Backend** | .NET 9 + cspurefix | FIX parsing runs in-process, direct access to AsciiParser, FixDefinitions |
| **Frontend** | React 18 + TypeScript | Industry standard, familiar tooling |
| **Build Tool** | Vite | Fast builds, simpler than Next.js for desktop apps |
| **State Management** | Redux Toolkit | Proven pattern for this domain |
| **Data Grid** | TanStack Table + @tanstack/virtual | MIT license, free row grouping, full cell customization |
| **Styling** | Tailwind CSS + shadcn/ui | Dark mode built-in, no retrofit pain |
| **Command Palette** | cmdk | Standard for modern apps (Linear, Vercel style) |
| **Icons** | Lucide React | Clean, consistent, tree-shakeable |

---

## Why Photino.NET?

### The In-Process Advantage

```
┌─────────────────────────────────────────────────────────────┐
│                   Photino.NET Process                        │
│                                                              │
│  ┌──────────────────────┐    ┌────────────────────────────┐ │
│  │   React Frontend     │◀──▶│   cspurefix Engine         │ │
│  │   (WebView2)         │    │   • AsciiParser            │ │
│  │                      │    │   • AsciiSegmentParser     │ │
│  │   • TanStack Table   │    │   • AsciiView              │ │
│  │   • Redux            │    │   • FixDefinitions         │ │
│  │   • Tailwind         │    │   • MessageFactory         │ │
│  └──────────────────────┘    └────────────────────────────┘ │
│            ▲                           │                     │
│            └─── IPC (SendWebMessage) ──┘                     │
└─────────────────────────────────────────────────────────────┘
```

**vs. Tauri (would require separate .NET process):**

```
┌─────────────────────┐         ┌─────────────────────┐
│  Tauri Process      │  gRPC/  │  .NET Sidecar       │
│  (Rust + WebView)   │◀───────▶│  (cspurefix)        │
│  React Frontend     │  HTTP   │  Separate process   │
└─────────────────────┘         └─────────────────────┘
     More complexity, IPC overhead, two processes to manage
```

### Comparison

| Factor | Photino.NET | Tauri + .NET Bridge | Electron |
|--------|-------------|---------------------|----------|
| .NET integration | **Native, in-process** | Sidecar via gRPC | Sidecar |
| Bundle size | ~5-10MB + .NET runtime | ~5-10MB + .NET runtime | 100MB+ |
| Memory | ~50-70MB | ~80-100MB (two processes) | 150-300MB |
| cspurefix access | Direct object access | Serialization required | Serialization required |
| Complexity | Single process | Two processes + IPC | Heavy |

---

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────────────────┐
│                        Photino.NET Application                           │
│                                                                          │
│  ┌────────────────────────────────────────────────────────────────────┐ │
│  │                         .NET Backend                                │ │
│  │                                                                     │ │
│  │  ┌─────────────────┐  ┌─────────────────┐  ┌────────────────────┐  │ │
│  │  │ ViewerService   │  │ FixDefinitions  │  │ File Cache         │  │ │
│  │  │ (message router)│  │ (loaded dicts)  │  │ (parsed files)     │  │ │
│  │  └────────┬────────┘  └─────────────────┘  └────────────────────┘  │ │
│  │           │                                                         │ │
│  │  ┌────────▼────────┐  ┌─────────────────┐  ┌────────────────────┐  │ │
│  │  │ AsciiParser     │  │ AsciiView       │  │ MessageFactory     │  │ │
│  │  │ (parse logs)    │  │ (tag access)    │  │ (JSON generation)  │  │ │
│  │  └─────────────────┘  └─────────────────┘  └────────────────────┘  │ │
│  └────────────────────────────────────────────────────────────────────┘ │
│                              ▲                                           │
│                              │ SendWebMessage / RegisterWebMessageHandler│
│                              ▼                                           │
│  ┌────────────────────────────────────────────────────────────────────┐ │
│  │                      React Frontend (WebView2)                      │ │
│  │                                                                     │ │
│  │  ┌─────────────────┐  ┌─────────────────┐  ┌────────────────────┐  │ │
│  │  │ Redux Store     │  │ PhotinoApi      │  │ UI Components      │  │ │
│  │  │ • files         │  │ (typed IPC)     │  │ • MessageGrid      │  │ │
│  │  │ • messages      │  │                 │  │ • DetailView       │  │ │
│  │  │ • filters       │  │                 │  │ • TagInspector     │  │ │
│  │  └─────────────────┘  └─────────────────┘  └────────────────────┘  │ │
│  └────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────────┘
```

---

## Communication Protocol

### IPC Message Format

All communication uses JSON messages via Photino's `SendWebMessage` / `window.external.sendMessage`.

```typescript
// Base message structure
interface IpcMessage {
  id: string;          // Unique request ID for correlation
  type: string;        // Message type (request/response discriminator)
  payload: unknown;    // Type-specific data
}

// Request from React → .NET
interface IpcRequest extends IpcMessage {
  type: 'parseFile' | 'getMessageDetail' | 'getTagMetadata' | 'listDictionaries' | 'sendToEndpoint';
}

// Response from .NET → React
interface IpcResponse extends IpcMessage {
  type: 'parseFileResult' | 'messageDetailResult' | 'error';
  success: boolean;
  error?: string;
}
```

### .NET Message Handler

```csharp
// Program.cs - Photino setup
public class Program
{
    private static ViewerService _viewerService = null!;

    [STAThread]
    static void Main(string[] args)
    {
        // Initialize cspurefix components
        _viewerService = new ViewerService();

        // Load default dictionaries
        _viewerService.LoadDictionary("FIX44", "dictionaries/FIX44.xml");
        _viewerService.LoadDictionary("FIX50SP2", "dictionaries/FIX50SP2.xml");

        var window = new PhotinoWindow()
            .SetTitle("FIX Log Viewer")
            .SetSize(1400, 900)
            .Center()
            .SetDevToolsEnabled(true)
            .RegisterWebMessageReceivedHandler(HandleMessage);

#if DEBUG
        // Development: connect to Vite dev server
        window.Load("http://localhost:5173");
#else
        // Production: serve built React app
        window.Load("wwwroot/index.html");
#endif

        window.WaitForClose();
    }

    private static void HandleMessage(object? sender, string message)
    {
        var window = (PhotinoWindow)sender!;

        try
        {
            var request = JsonSerializer.Deserialize<IpcRequest>(message);
            var response = _viewerService.HandleRequest(request);
            window.SendWebMessage(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            var errorResponse = new IpcResponse
            {
                Id = "unknown",
                Type = "error",
                Success = false,
                Error = ex.Message
            };
            window.SendWebMessage(JsonSerializer.Serialize(errorResponse));
        }
    }
}
```

### ViewerService (Core .NET Backend)

```csharp
public class ViewerService
{
    private readonly Dictionary<string, IFixDefinitions> _dictionaries = new();
    private readonly Dictionary<string, ParsedFile> _fileCache = new();

    public IpcResponse HandleRequest(IpcRequest request)
    {
        return request.Type switch
        {
            "parseFile" => HandleParseFile(request),
            "getMessageDetail" => HandleGetMessageDetail(request),
            "getTagMetadata" => HandleGetTagMetadata(request),
            "listDictionaries" => HandleListDictionaries(request),
            "sendToEndpoint" => HandleSendToEndpoint(request),
            _ => new IpcResponse { Id = request.Id, Success = false, Error = $"Unknown request type: {request.Type}" }
        };
    }

    private IpcResponse HandleParseFile(IpcRequest request)
    {
        var payload = JsonSerializer.Deserialize<ParseFileRequest>(request.Payload);

        // Check cache first
        var cacheKey = ComputeHash(payload.FilePath);
        if (_fileCache.TryGetValue(cacheKey, out var cached))
        {
            return new IpcResponse
            {
                Id = request.Id,
                Type = "parseFileResult",
                Success = true,
                Payload = new ParseFileResponse
                {
                    FileId = cacheKey,
                    Messages = cached.Summaries,
                    FromCache = true
                }
            };
        }

        // Parse the file using cspurefix
        var definitions = _dictionaries[payload.Dictionary];
        var parser = new AsciiParser();
        var segmentParser = new AsciiSegmentParser(definitions);

        var messages = new List<MessageSummary>();
        var rawMessages = new List<byte[]>();

        // Read and parse each message from the log file
        using var stream = File.OpenRead(payload.FilePath);
        // ... parsing logic using AsciiParser, AsciiView ...

        // Cache the result
        _fileCache[cacheKey] = new ParsedFile
        {
            Summaries = messages,
            RawMessages = rawMessages,
            Dictionary = payload.Dictionary
        };

        return new IpcResponse
        {
            Id = request.Id,
            Type = "parseFileResult",
            Success = true,
            Payload = new ParseFileResponse
            {
                FileId = cacheKey,
                Messages = messages,
                FromCache = false
            }
        };
    }

    private IpcResponse HandleGetMessageDetail(IpcRequest request)
    {
        var payload = JsonSerializer.Deserialize<GetMessageDetailRequest>(request.Payload);
        var cached = _fileCache[payload.FileId];
        var rawMessage = cached.RawMessages[payload.MessageIndex];
        var definitions = _dictionaries[cached.Dictionary];

        // Use AsciiView to extract all tags
        var parser = new AsciiParser();
        var view = parser.Parse(rawMessage, definitions);

        var tags = new List<TagDetail>();
        foreach (var tagPos in view.AllTags)
        {
            var fieldDef = definitions.TagToSimple.GetValueOrDefault(tagPos.Tag);
            tags.Add(new TagDetail
            {
                Tag = tagPos.Tag,
                Name = fieldDef?.Name ?? $"Tag{tagPos.Tag}",
                Value = view.GetString(tagPos.Tag),
                IsEnum = fieldDef?.EnumValues?.Any() ?? false,
                EnumDescription = GetEnumDescription(fieldDef, view.GetString(tagPos.Tag)),
                Component = GetContainingComponent(definitions, tagPos.Tag),
                Position = tagPos.Position
            });
        }

        // Generate JSON representation using MessageFactory
        var jsonObject = GenerateJsonRepresentation(view, definitions);

        return new IpcResponse
        {
            Id = request.Id,
            Type = "messageDetailResult",
            Success = true,
            Payload = new MessageDetailResponse
            {
                Raw = Encoding.ASCII.GetString(rawMessage),
                Json = jsonObject,
                Tags = tags
            }
        };
    }
}
```

### React IPC Client

```typescript
// src/lib/photinoApi.ts

type MessageHandler = (response: IpcResponse) => void;

class PhotinoApi {
  private pendingRequests = new Map<string, {
    resolve: (value: any) => void;
    reject: (error: Error) => void;
  }>();

  constructor() {
    // Listen for responses from .NET
    window.external.receiveMessage((json: string) => {
      const response: IpcResponse = JSON.parse(json);

      const pending = this.pendingRequests.get(response.id);
      if (pending) {
        this.pendingRequests.delete(response.id);
        if (response.success) {
          pending.resolve(response.payload);
        } else {
          pending.reject(new Error(response.error));
        }
      }
    });
  }

  private async send<TRequest, TResponse>(
    type: string,
    payload: TRequest
  ): Promise<TResponse> {
    const id = crypto.randomUUID();

    return new Promise((resolve, reject) => {
      this.pendingRequests.set(id, { resolve, reject });

      const request: IpcRequest = { id, type, payload };
      window.external.sendMessage(JSON.stringify(request));

      // Timeout after 30 seconds
      setTimeout(() => {
        if (this.pendingRequests.has(id)) {
          this.pendingRequests.delete(id);
          reject(new Error('Request timeout'));
        }
      }, 30000);
    });
  }

  // Typed API methods

  async parseFile(filePath: string, dictionary: string): Promise<ParseFileResponse> {
    return this.send('parseFile', { filePath, dictionary });
  }

  async getMessageDetail(fileId: string, messageIndex: number): Promise<MessageDetailResponse> {
    return this.send('getMessageDetail', { fileId, messageIndex });
  }

  async getTagMetadata(tag: number): Promise<TagMetadataResponse> {
    return this.send('getTagMetadata', { tag });
  }

  async listDictionaries(): Promise<DictionaryInfo[]> {
    return this.send('listDictionaries', {});
  }

  async sendToEndpoint(endpoint: string, messages: string[]): Promise<SendResult> {
    return this.send('sendToEndpoint', { endpoint, messages });
  }
}

// Singleton instance
export const photinoApi = new PhotinoApi();

// TypeScript declarations for Photino
declare global {
  interface Window {
    external: {
      sendMessage: (message: string) => void;
      receiveMessage: (callback: (message: string) => void) => void;
    };
  }
}
```

### Usage in React Components

```typescript
// src/features/files/fileUpload.ts
import { photinoApi } from '@/lib/photinoApi';
import { useAppDispatch } from '@/store';
import { fileLoaded, setActiveFile } from './filesSlice';

export function useFileUpload() {
  const dispatch = useAppDispatch();

  const uploadFile = async (filePath: string, dictionary: string) => {
    try {
      const result = await photinoApi.parseFile(filePath, dictionary);

      dispatch(fileLoaded({
        fileId: result.fileId,
        filePath,
        messages: result.messages,
        fromCache: result.fromCache
      }));

      dispatch(setActiveFile(result.fileId));

      return result;
    } catch (error) {
      console.error('Failed to parse file:', error);
      throw error;
    }
  };

  return { uploadFile };
}
```

---

## Native File Dialog Integration

Photino provides native file dialogs:

```csharp
// In ViewerService or via separate IPC handler
private IpcResponse HandleOpenFileDialog(IpcRequest request)
{
    var window = PhotinoWindow.Current; // Access current window

    var files = window.ShowOpenFile(
        title: "Select FIX Log File",
        defaultPath: Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        multiSelect: false,
        filters: new[] { ("FIX Log Files", "*.log;*.fix;*.txt"), ("All Files", "*.*") }
    );

    return new IpcResponse
    {
        Id = request.Id,
        Type = "openFileDialogResult",
        Success = true,
        Payload = new { FilePath = files?.FirstOrDefault() }
    };
}
```

```typescript
// React side
const openFile = async () => {
  const result = await photinoApi.openFileDialog();
  if (result.filePath) {
    await uploadFile(result.filePath, selectedDictionary);
  }
};
```

---

## Layout Design

### Primary Layout: Grid with Collapsible Detail Panel

The layout prioritizes the message grid while providing quick access to details. The detail panel expands/collapses below the grid.

```
┌─────────────────────────────────────────────────────────────────────────┐
│ [broker-a.log] [broker-b.log] [clearing.log] [+]       [⚙] [◐] [?]     │
├─────────────────────────────────────────────────────────────────────────┤
│ [⌘K]  [🔍 All Files]   [MsgType ▼] [Dir ▼]    234 msgs   [Copy] [▼]    │
├─────────────────────────────────────────────────────────────────────────┤
│ Time       │ Dir │ MsgType      │ Symbol │ ClOrdID   │ Side │ Status   │
│ 10:30:01.5 │ OUT │ NewOrder (D) │ AAPL   │ ORD-001   │ BUY  │          │
│ 10:30:01.7 │ IN  │ Ack (8)      │ AAPL   │ ORD-001   │ BUY  │ New      │
│ 10:30:02.1 │ IN  │ Fill (8)     │ AAPL   │ ORD-001   │ BUY  │ Partial  │
│▶10:30:02.3 │ IN  │ Fill (8)     │ AAPL   │ ORD-001   │ BUY  │ Filled   │ ← selected
│ 10:30:02.5 │ IN  │ DK (8)       │ AAPL   │ ORD-001   │ BUY  │ DoneDay  │
├─────────────────────────────────────────────────────────────────────────┤
│ [Summary] [JSON] [Tags] [Raw]                          [Copy] [Send ▼] │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│   ExecutionReport (8)                               ORD-001 → EX-123    │
│                                                                         │
│   AAPL  BUY  1,000 @ 185.50  FILLED                                     │
│                                                                         │
│   ClOrdID: ORD-001    OrderID: BRK-456    ExecID: EX-123                │
│   TradeDate: 2026-01-26    TransactTime: 10:30:02.341                   │
│                                                                         │
│   Parties: BROKER-X (Broker) │ CLIENT-A (Client) │ JSMITH (Trader)      │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
   [▲ Expand / ▼ Collapse detail panel]
```

### Layout Behavior

1. **Default**: Grid takes most space, detail panel collapsed or minimal height
2. **Click row**: Detail panel expands, shows Summary tab by default
3. **Switch tabs**: Content changes (Summary → JSON → Tags → Raw)
4. **Resize**: Drag border between grid and detail panel
5. **Collapse**: Double-click border or click collapse button to hide detail

### Quick Actions (No Detail Panel Required)

- **Multi-select rows**: Shift+click, Ctrl+click
- **Copy selected**: Toolbar button or Ctrl+C
- **Send to endpoint**: Toolbar dropdown
- **Filter by value**: Click hyperlinked ClOrdID in grid column

---

## Detail View Tabs

### 1. Summary Tab (Curated React Component)

A **designed, human-readable view** of the trade - not a grid, but a component showing the "shape" of the message:

```
┌─────────────────────────────────────────────────────────────────────────┐
│ ExecutionReport (8)                                  ORD-001 → EX-123   │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  AAPL                    BUY 1,000 @ 185.50                             │
│  Apple Inc.              Filled                                         │
│                                                                         │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐  ┌───────────────┐  │
│  │ ClOrdID     │  │ OrderID     │  │ ExecID      │  │ TradeDate     │  │
│  │ ORD-001     │  │ BRK-123456  │  │ EX-5        │  │ 2026-01-26    │  │
│  └─────────────┘  └─────────────┘  └─────────────┘  └───────────────┘  │
│                                                                         │
│  Parties:  Broker: BROKER-X  │  Client: CLIENT-A  │  Trader: JSmith    │
│                                                                         │
│  Instrument:  SecurityType: CS  │  Exchange: NYSE  │  Currency: USD     │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

- **Configurable per MsgType**: Different layouts for 35=D vs 35=8 vs 35=AE
- **Hyperlinked IDs**: Click ClOrdID → filter all messages with that ID
- **Shows key fields**: IDs, sizes, sides, dates, parties - the "shape" of the trade

### 2. JSON Tab (Structured Object View)

The **full parsed C# object** serialized as JSON - shows groups, components, nesting:

```json
{
  "header": {
    "BeginString": "FIX.4.4",
    "MsgType": "8",
    "SenderCompID": "BROKER-X"
  },
  "body": {
    "OrderID": "BRK-123456",
    "ClOrdID": "ORD-001",
    "Instrument": {
      "Symbol": "AAPL",
      "SecurityType": "CS"
    },
    "Parties": [
      { "PartyID": "BROKER-X", "PartyRole": "1" },
      { "PartyID": "CLIENT-A", "PartyRole": "13" }
    ]
  }
}
```

- **Shows structure**: "Where is this data? In a group? Which component?"
- **Collapsible tree**: Expand/collapse nodes
- **Generated from C# type**: MessageFactory creates typed object → serialize to JSON

### 3. Tags Tab (Filterable Grid)

A **flat list of all tags** with powerful filtering - for finding needles in haystacks:

```
┌─────────────────────────────────────────────────────────────────────────┐
│ Filter: [2026________] [enums] [header] [groups]           238 tags    │
├─────────────────────────────────────────────────────────────────────────┤
│ Tag  │ Name           │ Value              │ Component    │ Pos        │
├──────┼────────────────┼────────────────────┼──────────────┼────────────┤
│ 60   │ TransactTime   │ 20260126-10:30:01  │ Body         │ 15         │
│ 75   │ TradeDate      │ 20260126           │ Body         │ 42         │
│ 5765 │ CustomDate     │ 2026-01-26         │ CustomGrp[0] │ 89         │
└─────────────────────────────────────────────────────────────────────────┘
```

- **Search anything**: Type "2026" → finds all tags containing that value
- **Filter by type**: "enums only", "header", "groups"
- **Hundreds of tags**: Virtual scrolling, fast debounced filtering
- **Tag metadata**: Click [?] icon → show tag definition, enum values

### 4. Raw Tab

The original FIX message with SOH delimiters shown as `|`:

```
8=FIX.4.4|9=256|35=8|49=BROKER-X|56=CLIENT-A|34=123|52=20260126-10:30:02.341|
37=BRK-123456|11=ORD-001|17=EX-123|150=2|39=2|55=AAPL|54=1|38=1000|44=185.50|
10=089|
```

---

## Global Search (Ctrl+Shift+F)

Search across **all loaded files** - essential when you can't remember which log contains a specific order.

### Global Search Panel

```
┌─────────────────────────────────────────────────────────────────────────┐
│ Search all files: [ORD-12345_______________________] [×]                │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  broker-a.log (3 matches)                                        [−]   │
│  ├─ 10:30:01.5  NewOrder (D)   ORD-12345  AAPL  BUY 1000               │
│  ├─ 10:30:01.8  ExecRpt (8)    ORD-12345  AAPL  BUY 1000  New          │
│  └─ 10:30:02.1  ExecRpt (8)    ORD-12345  AAPL  BUY 1000  Filled       │
│                                                                         │
│  broker-b.log (1 match)                                          [−]   │
│  └─ 10:31:05.2  TradeCap (AE)  ORD-12345  AAPL  BUY 1000               │
│                                                                         │
│  clearing.log (2 matches)                                        [−]   │
│  ├─ 10:32:00.0  Alloc (J)      ORD-12345  AAPL  BUY 500  ACCT-1        │
│  └─ 10:32:00.1  Alloc (J)      ORD-12345  AAPL  BUY 500  ACCT-2        │
│                                                                         │
│                                               6 results in 3 files      │
└─────────────────────────────────────────────────────────────────────────┘
```

### Behavior

- **Ctrl+Shift+F** or click [🔍 All Files] → opens search panel as overlay
- **Searches all cached/loaded files** - not just active tab
- **Groups results by file** - collapsible sections
- **Click a result** → switches to that file tab, selects message, shows detail
- **Searches all tag values** - any tag containing the search term
- **Optional specific tag search**: `11=ORD-12345` or `55=AAPL`

### Command Palette Integration

The command palette (⌘K) also supports global search:

```
┌─────────────────────────────────────────────────────────────────────────┐
│ > ORD-12345                                                             │
├─────────────────────────────────────────────────────────────────────────┤
│ 🔍 Search in current file                                               │
│ 🔍 Search in all files (6 matches in 3 files)                     ←     │
│ ─────────────────────────────────────────────────────────────────────   │
│ 📁 Switch to: broker-a.log                                              │
│ 📁 Switch to: broker-b.log                                              │
│ ─────────────────────────────────────────────────────────────────────   │
│ ⚡ Filter current view to ORD-12345                                     │
│ ⚡ Copy all messages with ORD-12345                                     │
└─────────────────────────────────────────────────────────────────────────┘
```

---

## Multi-File Cache Architecture

### Redux State

```typescript
interface RootState {
  // Multi-file management
  files: {
    byId: Record<string, CachedFile>;
    activeId: string | null;
    tabOrder: string[];  // controls tab display order
  };

  // Messages for ACTIVE file only (to save memory)
  messages: {
    summaries: MessageSummary[];  // from active file
    selectedIndex: number | null;
    selectedIndices: number[];  // for multi-select
    filteredIndices: number[];  // after applying filters
  };

  // Global search
  search: {
    isOpen: boolean;
    query: string;
    results: SearchResultGroup[];
    isSearching: boolean;
  };

  // Filters (apply to active file)
  filters: {
    text: string;
    msgTypes: string[];
    directions: ('IN' | 'OUT')[];
    presets: FilterPreset[];
    activePreset: string | null;
  };

  // UI state
  ui: {
    detailPanelHeight: number;
    detailPanelCollapsed: boolean;
    activeDetailTab: 'summary' | 'json' | 'tags' | 'raw';
    theme: 'light' | 'dark' | 'system';
  };
}

interface CachedFile {
  id: string;           // hash of file content
  name: string;         // display name (filename)
  path: string;         // full path
  loadedAt: number;     // timestamp
  dictionary: string;   // which dictionary was used
  messageCount: number; // total messages
  // Note: actual message data stays in .NET backend cache
}

interface SearchResultGroup {
  fileId: string;
  fileName: string;
  matches: SearchMatch[];
  isCollapsed: boolean;
}

interface SearchMatch {
  messageIndex: number;
  summary: MessageSummary;
  matchedTags: { tag: number; value: string }[];
}
```

### .NET Backend Cache

```csharp
public class FileCache
{
    // Keyed by file content hash
    private readonly Dictionary<string, CachedFile> _files = new();

    public class CachedFile
    {
        public string Id { get; init; }           // content hash
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public string Dictionary { get; init; }
        public DateTime LoadedAt { get; init; }

        // Actual data - stays in .NET memory
        public List<MessageSummary> Summaries { get; init; }
        public List<byte[]> RawMessages { get; init; }  // for detail view
    }
}
```

### IPC: Global Search

```csharp
// Request
public record GlobalSearchRequest(
    string Query,
    string[]? FileIds = null  // null = search all
);

// Response
public record GlobalSearchResponse(
    string Query,
    int TotalMatches,
    SearchResultGroup[] Groups
);

public record SearchResultGroup(
    string FileId,
    string FileName,
    SearchMatch[] Matches
);

public record SearchMatch(
    int MessageIndex,
    MessageSummary Summary,
    MatchedTag[] MatchedTags
);

public record MatchedTag(int Tag, string Value);
```

---

## Summary Component Configuration

The Summary tab renders differently based on message type:

```typescript
// config/summaryLayouts.ts
interface SummaryLayout {
  title: (msg: MessageDetail) => string;
  subtitle: (msg: MessageDetail) => string;
  primaryDisplay: PrimaryDisplayConfig;
  sections: SectionConfig[];
}

interface PrimaryDisplayConfig {
  symbol: number;      // tag for symbol (55)
  side: number;        // tag for side (54)
  quantity: number;    // tag for qty (38)
  price: number;       // tag for price (44)
  status?: number;     // tag for status (39 for exec, etc.)
}

interface SectionConfig {
  title: string;
  fields?: FieldConfig[];
  type?: 'parties' | 'legs' | 'custom';  // special rendering
}

interface FieldConfig {
  label: string;
  tag: number;
  format?: 'string' | 'number' | 'price' | 'date' | 'timestamp' | 'side' | 'ordStatus';
  hyperlink?: boolean;  // click to filter
}

const layouts: Record<string, SummaryLayout> = {
  // ExecutionReport (8)
  '8': {
    title: (msg) => `ExecutionReport (8)`,
    subtitle: (msg) => `${msg.tags[11]} → ${msg.tags[17]}`,
    primaryDisplay: { symbol: 55, side: 54, quantity: 38, price: 44, status: 39 },
    sections: [
      {
        title: 'IDs',
        fields: [
          { label: 'ClOrdID', tag: 11, hyperlink: true },
          { label: 'OrderID', tag: 37, hyperlink: true },
          { label: 'ExecID', tag: 17 },
        ]
      },
      { title: 'Parties', type: 'parties' },
      {
        title: 'Dates',
        fields: [
          { label: 'TradeDate', tag: 75, format: 'date' },
          { label: 'TransactTime', tag: 60, format: 'timestamp' },
        ]
      }
    ]
  },

  // NewOrderSingle (D)
  'D': {
    title: (msg) => `NewOrderSingle (D)`,
    subtitle: (msg) => msg.tags[11],
    primaryDisplay: { symbol: 55, side: 54, quantity: 38, price: 44 },
    sections: [
      {
        title: 'Order Details',
        fields: [
          { label: 'ClOrdID', tag: 11, hyperlink: true },
          { label: 'OrdType', tag: 40, format: 'ordType' },
          { label: 'TimeInForce', tag: 59, format: 'tif' },
        ]
      },
      { title: 'Parties', type: 'parties' },
    ]
  },

  // Default fallback
  'default': {
    title: (msg) => `${msg.msgTypeName} (${msg.msgType})`,
    subtitle: (msg) => msg.tags[11] || '',
    primaryDisplay: { symbol: 55, side: 54, quantity: 38, price: 44 },
    sections: []
  }
};
```

---

## Project Structure

```
PureFix.Viewer/
├── PureFix.Viewer/                    # .NET Photino application
│   ├── Program.cs                     # Photino window setup, message routing
│   ├── Services/
│   │   ├── ViewerService.cs           # Core service, handles all IPC requests
│   │   ├── FileParserService.cs       # FIX log parsing using cspurefix
│   │   ├── DictionaryService.cs       # Dictionary loading and management
│   │   └── EndpointService.cs         # Dev endpoint HTTP calls
│   ├── Models/
│   │   ├── IpcMessages.cs             # Request/Response types
│   │   ├── MessageSummary.cs          # Summary for grid display
│   │   ├── TagDetail.cs               # Full tag information
│   │   └── Configuration.cs           # App configuration model
│   ├── wwwroot/                       # Built React app (production)
│   ├── appsettings.json               # Configuration
│   └── PureFix.Viewer.csproj
│
├── purefix-viewer-ui/                 # React frontend (Vite)
│   ├── src/
│   │   ├── components/
│   │   │   ├── layout/
│   │   │   │   ├── AppShell.tsx
│   │   │   │   ├── FileTabs.tsx
│   │   │   │   ├── Toolbar.tsx
│   │   │   │   └── DetailPanel.tsx
│   │   │   ├── grid/
│   │   │   │   ├── MessageGrid.tsx
│   │   │   │   ├── columns.tsx
│   │   │   │   └── cellRenderers.tsx
│   │   │   ├── detail/
│   │   │   │   ├── DetailView.tsx
│   │   │   │   ├── SummaryTab.tsx      # Curated trade view
│   │   │   │   ├── JsonTab.tsx         # Structured object tree
│   │   │   │   ├── TagsTab.tsx         # Filterable tag grid
│   │   │   │   └── RawTab.tsx          # Raw FIX message
│   │   │   ├── search/
│   │   │   │   ├── GlobalSearch.tsx    # Ctrl+Shift+F panel
│   │   │   │   └── SearchResults.tsx
│   │   │   ├── command/
│   │   │   │   └── CommandPalette.tsx  # ⌘K
│   │   │   └── ui/                     # shadcn components
│   │   ├── features/
│   │   │   ├── files/
│   │   │   │   ├── filesSlice.ts       # Multi-file cache
│   │   │   │   └── useFileUpload.ts
│   │   │   ├── messages/
│   │   │   │   ├── messagesSlice.ts
│   │   │   │   └── useMessageDetail.ts
│   │   │   ├── search/
│   │   │   │   ├── searchSlice.ts      # Global search state
│   │   │   │   └── useGlobalSearch.ts
│   │   │   ├── filters/
│   │   │   │   ├── filtersSlice.ts
│   │   │   │   └── filterParser.ts
│   │   │   └── config/
│   │   │       ├── configSlice.ts
│   │   │       └── summaryLayouts.ts   # Per-MsgType layouts
│   │   ├── lib/
│   │   │   ├── photinoApi.ts           # IPC client
│   │   │   ├── store.ts                # Redux store setup
│   │   │   └── theme.ts
│   │   ├── hooks/
│   │   │   ├── useMessageSelection.ts
│   │   │   ├── useDebouncedFilter.ts
│   │   │   └── useKeyboardShortcuts.ts
│   │   ├── App.tsx
│   │   ├── main.tsx
│   │   └── index.css
│   ├── package.json
│   ├── vite.config.ts
│   ├── tailwind.config.js
│   └── tsconfig.json
│
├── dictionaries/                       # FIX XML dictionaries
│   ├── FIX44.xml
│   └── FIX50SP2.xml
│
└── PureFix.Viewer.sln
```

---

## csproj: NuGet vs Project Reference Toggle

The viewer can reference cspurefix either via **NuGet packages** (stable releases) or **project references** (active development). This follows the same pattern as [purefix-standalone-demo](https://github.com/user/purefix-standalone-demo).

### Option 1: MSBuild Property Toggle (Recommended)

```xml
<!-- PureFix.Viewer.csproj -->
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <!--
      Toggle between NuGet packages and local project references.
      Set to 'true' when developing against local cspurefix changes.

      Usage:
        dotnet build                          # Uses NuGet (default)
        dotnet build /p:UseLocalPureFix=true  # Uses project references
    -->
    <UseLocalPureFix>false</UseLocalPureFix>

    <!-- Path to local cspurefix repo (adjust as needed) -->
    <CsPureFixRoot>$(MSBuildThisFileDirectory)..\..\cspurefix</CsPureFixRoot>
  </PropertyGroup>

  <!-- Photino.NET -->
  <ItemGroup>
    <PackageReference Include="Photino.NET" Version="4.0.16" />
  </ItemGroup>

  <!-- cspurefix: NuGet packages (default) -->
  <ItemGroup Condition="'$(UseLocalPureFix)' != 'true'">
    <PackageReference Include="PureFix.Buffer" Version="0.2.9-beta" />
    <PackageReference Include="PureFix.Dictionary" Version="0.2.9-beta" />
    <PackageReference Include="PureFix.LogMessageParser" Version="0.2.9-beta" />
    <PackageReference Include="PureFix.Types.Core" Version="0.2.9-beta" />
    <!-- Add specific dictionary type packages as needed -->
    <PackageReference Include="PureFix.Types.FIX44" Version="0.2.9-beta" />
    <PackageReference Include="PureFix.Types.FIX50SP2" Version="0.2.9-beta" />
  </ItemGroup>

  <!-- cspurefix: Local project references (for development) -->
  <ItemGroup Condition="'$(UseLocalPureFix)' == 'true'">
    <ProjectReference Include="$(CsPureFixRoot)\PureFix.Buffer\PureFix.Buffer.csproj" />
    <ProjectReference Include="$(CsPureFixRoot)\PureFix.Dictionary\PureFix.Dictionary.csproj" />
    <ProjectReference Include="$(CsPureFixRoot)\PureFix.LogMessageParser\PureFix.LogMessageParser.csproj" />
    <ProjectReference Include="$(CsPureFixRoot)\PureFix.Types.Core\PureFix.Types.Core.csproj" />
    <ProjectReference Include="$(CsPureFixRoot)\PureFix.Types.FIX44\PureFix.Types.FIX44.csproj" />
    <ProjectReference Include="$(CsPureFixRoot)\PureFix.Types.FIX50SP2\PureFix.Types.FIX50SP2.csproj" />
  </ItemGroup>

  <!-- Other dependencies -->
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.0" />
  </ItemGroup>

</Project>
```

### Option 2: Comment Toggle (Simpler)

Same pattern as the standalone demo - just comment/uncomment:

```xml
<ItemGroup>
  <!--
    PureFix packages from NuGet (default for release/testing)
    To develop against local cspurefix changes:
      1. Comment out the PackageReference lines below
      2. Uncomment the ProjectReference lines
      3. Run: dotnet build
  -->
  <PackageReference Include="PureFix.Buffer" Version="0.2.9-beta" />
  <PackageReference Include="PureFix.Dictionary" Version="0.2.9-beta" />
  <PackageReference Include="PureFix.LogMessageParser" Version="0.2.9-beta" />

  <!-- Local project references (uncomment for development)
  <ProjectReference Include="../cspurefix/PureFix.Buffer/PureFix.Buffer.csproj" />
  <ProjectReference Include="../cspurefix/PureFix.Dictionary/PureFix.Dictionary.csproj" />
  <ProjectReference Include="../cspurefix/PureFix.LogMessageParser/PureFix.LogMessageParser.csproj" />
  -->
</ItemGroup>
```

### Development Scenarios

| Scenario | Command |
|----------|---------|
| Normal development (stable engine) | `dotnet build` |
| Testing engine changes | `dotnet build /p:UseLocalPureFix=true` |
| CI/CD release build | `dotnet publish -c Release` |

---

## Development Workflow

### Prerequisites

- .NET 9 SDK
- Node.js 20+
- pnpm (or npm/yarn)

### Development Mode

**Terminal 1: Start Vite dev server**
```bash
cd purefix-viewer-ui
pnpm install
pnpm dev  # Starts on http://localhost:5173
```

**Terminal 2: Start Photino app (DEBUG mode)**
```bash
cd PureFix.Viewer
dotnet run
# Opens window pointing to http://localhost:5173
# Hot reload for React, restart .NET for backend changes
```

### Production Build

```bash
# Build React app
cd purefix-viewer-ui
pnpm build  # Outputs to dist/

# Copy to .NET wwwroot
cp -r dist/* ../PureFix.Viewer/wwwroot/

# Build .NET app
cd ../PureFix.Viewer
dotnet publish -c Release -r win-x64 --self-contained
```

### Developing with Local cspurefix

When you need to test changes to the FIX engine without publishing to NuGet:

```bash
# Terminal 1: Vite dev server
cd purefix-viewer-ui && pnpm dev

# Terminal 2: Photino with local cspurefix project references
cd PureFix.Viewer
dotnet run /p:UseLocalPureFix=true
```

Or set it in your IDE's launch profile:

```json
// launchSettings.json
{
  "profiles": {
    "PureFix.Viewer": {
      "commandName": "Project",
      "environmentVariables": {}
    },
    "PureFix.Viewer (Local Engine)": {
      "commandName": "Project",
      "commandLineArgs": "/p:UseLocalPureFix=true"
    }
  }
}
```

---

## Development & Debugging

### Debugging Architecture

Photino.NET uses WebView2 on Windows, which means **two separate debugging contexts**:

```
┌─────────────────────────────────────────────────────────────────────────┐
│                         Debugging Setup                                  │
│                                                                          │
│  ┌─────────────────────────────┐    ┌─────────────────────────────────┐ │
│  │   Visual Studio 2022        │    │   Photino Window                │ │
│  │                             │    │                                 │ │
│  │   Debug .NET code:          │    │   Debug TypeScript/React:       │ │
│  │   • F5 to start             │    │   • Press F12 → Edge DevTools   │ │
│  │   • Breakpoints in C#       │    │   • Breakpoints in TS (source   │ │
│  │   • Watch ViewerService     │    │     maps enabled by Vite)       │ │
│  │   • Step through parsing    │    │   • Console.log for IPC         │ │
│  │                             │    │   • Network tab for requests    │ │
│  └─────────────────────────────┘    └─────────────────────────────────┘ │
│                                                                          │
│  Note: Cannot debug both .NET and TypeScript simultaneously in VS.       │
│  Use two separate debuggers (VS for .NET, DevTools for TS).              │
└─────────────────────────────────────────────────────────────────────────┘
```

### Debugging .NET Code

1. Open `PureFix.Viewer.sln` in Visual Studio 2022
2. Set breakpoints in `ViewerService.cs`, `FileParserService.cs`, etc.
3. Press F5 to start debugging
4. The Photino window opens, pointing to Vite dev server
5. Interact with the UI → breakpoints hit in .NET code

### Debugging TypeScript/React

1. With the Photino app running, press **F12** in the window
2. Edge DevTools opens (same as Chrome DevTools)
3. Go to **Sources** tab → find your `.tsx` files (Vite provides source maps)
4. Set breakpoints, inspect variables, use console

### IPC Boundary Logging

Add logging on both sides to trace message flow:

```csharp
// C# - ViewerService.cs
private static void HandleMessage(object? sender, string message)
{
    var window = (PhotinoWindow)sender!;

    // Log incoming request
    Console.WriteLine($"[.NET ← React] {message[..Math.Min(200, message.Length)]}...");

    try
    {
        var request = JsonSerializer.Deserialize<IpcRequest>(message);
        var response = _viewerService.HandleRequest(request);
        var responseJson = JsonSerializer.Serialize(response);

        // Log outgoing response
        Console.WriteLine($"[.NET → React] {response.Type} (success={response.Success})");

        window.SendWebMessage(responseJson);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[.NET ERROR] {ex.Message}");
        // ... error handling
    }
}
```

```typescript
// TypeScript - photinoApi.ts
private async send<T>(type: string, payload: unknown): Promise<T> {
  const id = crypto.randomUUID();

  // Log outgoing request
  console.log(`[React → .NET] ${type}`, payload);

  return new Promise((resolve, reject) => {
    this.pendingRequests.set(id, {
      resolve: (data) => {
        // Log incoming response
        console.log(`[React ← .NET] ${type} response`, data);
        resolve(data);
      },
      reject
    });

    window.external.sendMessage(JSON.stringify({ id, type, payload }));
  });
}
```

### Testing cspurefix Independently

The FIX engine can be unit tested separately without the UI:

```bash
# Run cspurefix unit tests
cd cspurefix
dotnet test PureFix.Test.ModularTypes

# Or test specific parsing scenarios
dotnet run --project PureFix.ConsoleApp -- parse test-file.log
```

### Common Debugging Scenarios

| Issue | Debug Approach |
|-------|----------------|
| File not parsing | Breakpoint in `FileParserService.ParseFile()`, check `AsciiParser` output |
| Wrong tag values | Breakpoint in `HandleGetMessageDetail()`, inspect `AsciiView` |
| UI not updating | DevTools console, check Redux state with React DevTools extension |
| IPC timeout | Check both console outputs, verify message format matches |
| Missing dictionary | Breakpoint in `DictionaryService.LoadDictionary()` |

---

## Configuration

```json
// appsettings.json
{
  "Viewer": {
    "Dictionaries": [
      { "Id": "FIX44", "Path": "dictionaries/FIX44.xml", "Default": true },
      { "Id": "FIX50SP2", "Path": "dictionaries/FIX50SP2.xml" }
    ],
    "SummaryTags": {
      "8": [167, 55, 48],
      "D": [55, 54, 38, 44, 11],
      "default": [55, 11]
    },
    "HyperlinkedTags": [11, 37, 17],
    "ColorCoding": {
      "D": { "Bg": "#dcfce7", "Text": "#166534" },
      "8": { "Bg": "#dbeafe", "Text": "#1e40af" },
      "9": { "Bg": "#fee2e2", "Text": "#991b1b" },
      "j": { "Bg": "#fee2e2", "Text": "#991b1b" },
      "3": { "Bg": "#fef3c7", "Text": "#92400e" }
    },
    "FilterPresets": [
      { "Name": "All Trades", "Filter": "35=D|8|F|G" },
      { "Name": "No Session", "Filter": "-35=0|A|1|2|3|4|5" },
      { "Name": "Rejects Only", "Filter": "35=9|j|3" }
    ],
    "DevEndpoints": [
      {
        "Name": "Local Booking Service",
        "Url": "http://localhost:5000/api/fix/submit",
        "Method": "POST"
      }
    ]
  }
}
```

---

## Implementation Phases

### Phase 1: Scaffold & Basic Grid (MVP)

- [ ] Create PureFix.Viewer .NET project with Photino.NET 4.0.16
- [ ] Set up purefix-viewer-ui with Vite + React + TypeScript + Tailwind
- [ ] Implement IPC message protocol (PhotinoApi class)
- [ ] Implement ViewerService with parseFile handler
- [ ] Native file open dialog integration
- [ ] Basic message grid with TanStack Table + virtual scrolling
- [ ] Collapsible detail panel with Raw tab
- [ ] Theme support (dark/light via Tailwind + shadcn)
- [ ] Single file load/display workflow

### Phase 2: Detail Views & Multi-File

- [ ] Tags tab - filterable grid of all tags
- [ ] JSON tab - structured object tree view
- [ ] Summary tab - curated React component per MsgType
- [ ] Multi-file cache in .NET backend
- [ ] File tabs in UI
- [ ] Switch between files (re-render grid from cache)

### Phase 3: Search & Filtering

- [ ] Debounced filtering in message grid
- [ ] Tags tab filtering (text, enums, header, groups)
- [ ] Global search (Ctrl+Shift+F) across all loaded files
- [ ] Command palette (⌘K) with search integration
- [ ] Filter presets (All Trades, No Session, Rejects)

### Phase 4: Power User Features

- [ ] Hyperlinked tags - click to filter
- [ ] Row grouping by MsgType
- [ ] Color coding by message type (configurable)
- [ ] Multi-select rows
- [ ] Copy to clipboard (single or batch)
- [ ] Keyboard shortcuts (Ctrl+C, Ctrl+Shift+F, etc.)

### Phase 5: Integration Features

- [ ] Dev endpoint sending (POST to configurable URLs)
- [ ] Message editing/patching (modify ClOrdID with timestamp, etc.)
- [ ] Export filtered results to CSV/JSON
- [ ] Summary layout configuration per broker/MsgType

### Phase 6: Polish & Distribution

- [ ] Application icon and branding
- [ ] Windows installer (MSIX or similar)
- [ ] Settings persistence (window size, theme, recent files)
- [ ] Error handling and user feedback
- [ ] Performance optimization for large files (50k+ messages)

### Future: Live Mode

- [ ] SignalR client in React
- [ ] IMessagePublisher integration in cspurefix
- [ ] Real-time message streaming
- [ ] Pause/resume/filter live stream

---

## Key Benefits of This Architecture

1. **Zero Serialization for Parsing** - cspurefix runs in-process, direct memory access to parsed data

2. **Single Process** - No sidecar server, no port management, no process coordination

3. **Native File Dialogs** - Uses OS file picker, feels like a native app

4. **Leverages Existing Code** - AsciiParser, AsciiView, FixDefinitions, MessageFactory all work directly

5. **Simple Deployment** - Single executable (with .NET runtime or self-contained)

6. **Debug Both Sides** - Visual Studio for .NET, browser DevTools (F12) for React

---

## TypeScript API Types

```typescript
// src/types/api.ts

export interface ParseFileResponse {
  fileId: string;
  messages: MessageSummary[];
  fromCache: boolean;
  parseErrors?: ParseError[];
}

export interface MessageSummary {
  index: number;
  seqNum: number;
  msgType: string;
  msgTypeName: string;
  direction: 'IN' | 'OUT';
  timestamp: string;
  senderCompId: string;
  targetCompId: string;
  // Dynamic summary tags based on config
  summaryFields: Record<string, string>;
}

export interface MessageDetailResponse {
  raw: string;
  json: Record<string, unknown>;
  tags: TagDetail[];
}

export interface TagDetail {
  tag: number;
  name: string;
  value: string;
  rawValue: string;
  isEnum: boolean;
  enumDescription?: string;
  component?: string;
  position: number;
}

export interface TagMetadataResponse {
  tag: number;
  name: string;
  type: string;
  description?: string;
  enumValues?: { value: string; description: string }[];
  usedInComponents: string[];
  usedInMessages: string[];
}

export interface DictionaryInfo {
  id: string;
  name: string;
  version: string;
  messageCount: number;
  componentCount: number;
}

export interface ParseError {
  line: number;
  message: string;
  rawContent?: string;
}
```

---

## Related Documents

- [Message Publisher & Viewer Roadmap](./message-publisher-viewer-roadmap.md)
- [Previous Tauri-based Design](./fix-viewer-design.md) (superseded)
- [Type Registry Design](./TypeRegistryDesign.md)

---

## References

- [Photino.NET GitHub](https://github.com/AlizerUncworworWorked/photino.NET)
- [Photino Samples](https://github.com/tryphotino/photino.Samples)
- [Photino Documentation](https://docs.tryphotino.io/)
- [TanStack Table](https://tanstack.com/table)
- [shadcn/ui](https://ui.shadcn.com/)
- [cmdk](https://cmdk.paco.me/)
