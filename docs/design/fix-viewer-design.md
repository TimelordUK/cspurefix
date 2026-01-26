# FIX Log Viewer - Detailed Design Document

## Overview

A standalone desktop application for viewing, filtering, and analyzing FIX protocol log files. Built with React + Tauri, designed for power users who need to quickly navigate large FIX logs, inspect message details, and optionally interact with external services.

**Primary Use Case:** Upload FIX log files from production systems, rapidly filter to find relevant messages, inspect tag details, and optionally forward messages to development endpoints for testing.

---

## Technology Stack

| Layer | Technology | Rationale |
|-------|------------|-----------|
| **Desktop Shell** | Tauri 2.0 | Small bundle (3-10MB), low memory, fast startup, stable since Oct 2024 |
| **Frontend** | React 18 + TypeScript | Industry standard, familiar tooling |
| **State Management** | Redux Toolkit | Proven pattern for this domain (files, messages, selection) |
| **Data Grid** | TanStack Table + @tanstack/virtual | MIT license, free row grouping, full cell customization |
| **Styling** | Tailwind CSS + shadcn/ui | Dark mode support built-in, no retrofit pain |
| **Command Palette** | cmdk | Standard for modern apps (Linear, Vercel style) |
| **Data Fetching** | TanStack Query | Server state management, caching |
| **Icons** | Lucide React | Clean, consistent, tree-shakeable |

### Why Not AG Grid?

AG Grid Community lacks row grouping (Enterprise only at $999/dev/year). TanStack Table provides:
- Row grouping for free
- Complete cell rendering control (essential for FIX-specific color coding)
- Virtual scrolling via @tanstack/virtual
- Smaller bundle size

### Why Tauri Over Electron?

| Metric | Electron | Tauri 2.0 |
|--------|----------|-----------|
| Bundle size | 100MB+ | 3-10MB |
| Memory (idle) | 150-300MB | 30-50MB |
| Startup | 1-2 sec | <500ms |

For a utility tool that may run alongside trading systems, resource efficiency matters.

---

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────────────────┐
│                           Tauri Shell                                    │
│  ┌───────────────────────────────────────────────────────────────────┐  │
│  │                        React Application                           │  │
│  │                                                                    │  │
│  │  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────────┐   │  │
│  │  │ Redux Store │  │ TanStack    │  │ Service Adapters        │   │  │
│  │  │ - files     │  │ Query       │  │ - FIX Parser (backend)  │   │  │
│  │  │ - messages  │  │ - parse     │  │ - Trade Service (opt)   │   │  │
│  │  │ - selection │  │ - details   │  │ - Instrument Svc (opt)  │   │  │
│  │  │ - filters   │  │ - services  │  │ - Dev Endpoint (config) │   │  │
│  │  └─────────────┘  └─────────────┘  └─────────────────────────┘   │  │
│  │                                                                    │  │
│  │  ┌──────────────────────────────────────────────────────────────┐ │  │
│  │  │                    UI Components                              │ │  │
│  │  │  ┌─────────────┐ ┌───────────────────┐ ┌──────────────────┐  │ │  │
│  │  │  │ Message     │ │ Detail Panel      │ │ Tag Inspector    │  │ │  │
│  │  │  │ Grid        │ │ (tabs)            │ │ (drawer)         │  │ │  │
│  │  │  └─────────────┘ └───────────────────┘ └──────────────────┘  │ │  │
│  │  └──────────────────────────────────────────────────────────────┘ │  │
│  └───────────────────────────────────────────────────────────────────┘  │
│                                                                          │
│  Tauri Plugins: fs, clipboard, http                                      │
└─────────────────────────────────────────────────────────────────────────┘
```

---

## Layout Design

### Rationale

The previous 3-panel vertical split with growing middle tabs had these issues:
1. Middle panel became a dumping ground for different detail views
2. Constant border adjustments (responsiveness challenges)
3. Tag inspector competed for horizontal space

### New Approach: Hybrid Layout

```
┌─────────────────────────────────────────────────────────────────────────┐
│ [File1.log] [File2.log] [+Upload]                     [⚙] [Theme] [?]   │
├─────────────────────────────────────────────────────────────────────────┤
│ [⌘K] Search messages, filter by tag, jump to... ____________           │
├─────────────────────────────────────────────────────────────────────────┤
│ Filters: [MsgType ▼] [Direction ▼] [Session ▼] [Clear] | Showing: 234  │
├───────────────────────┬─────────────────────────────────────────────────┤
│                       │                                                 │
│   Message List        │   Detail View                                   │
│   (TanStack Table)    │   [Summary] [JSON] [Raw]                        │
│                       │   ───────────────────────────────────────────   │
│   • Color-coded rows  │                                                 │
│   • Debounced filter  │   (Content based on selected tab)               │
│   • Grouping          │                                                 │
│                       │                                                 │
│                       │                                       [Services]│
├───────────────────────┴─────────────────────────────────────────────────┤
│ Tag Inspector (collapsible drawer)                               [▼ ▲] │
│ ┌─────────────────────────────────────────────────────────────────────┐ │
│ │ Filter: [________] [enums only] [header] [instrument]               │ │
│ │ Tag │ Name        │ Value           │ Component   │ Position        │ │
│ │ 8   │ BeginString │ FIX.4.4         │ Header      │ 0               │ │
│ │ 35  │ MsgType     │ D (NewOrderSin) │ Header      │ 2     [?]       │ │
│ └─────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────────┘
```

### Key Layout Decisions

1. **File Tabs at Top** - Leverages your multi-file cache; instant switching between loaded files

2. **Command Palette (⌘K)** - Your sophisticated filter syntax (`8-35`, `msgType`, `header`, `instrument`) becomes the primary power-user interface. Examples:
   - `35=D` - Filter to NewOrderSingle messages
   - `11=ORD-001` - Find all messages with specific ClOrdID
   - `header` - Show only header tags in inspector
   - `enum` - Show only enum fields
   - `2017` - Search for tag value containing "2017"

3. **Two Primary Panels** - Message List (left) and Detail View (right), resizable

4. **Tag Inspector as Bottom Drawer** - Chrome DevTools pattern; doesn't fight for horizontal space; can be collapsed when not needed; expands upward when you need more tag space

5. **Services as Slide-Out** - Trade/Instrument service lookups are contextual; appear when you click a hyperlinked value (e.g., order ID); don't consume permanent space

6. **Canned Filters as Dropdown** - "All Trades", "No Session Messages", "Rejects Only" configurable presets

### Responsive Behavior

| Breakpoint | Layout |
|------------|--------|
| < 1024px | Single panel with navigation tabs |
| 1024-1440px | Two panels, tag inspector as modal |
| > 1440px | Full layout as shown above |

---

## Component Hierarchy

```
<App>
  <ThemeProvider>
    <Provider store={store}>
      <QueryClientProvider>
        <AppShell>
          <FileTabs />
          <CommandPalette />
          <Toolbar>
            <FilterBar />
            <QuickFilters />
          </Toolbar>
          <PanelGroup direction="horizontal">
            <Panel>
              <MessageGrid />
            </Panel>
            <PanelResizeHandle />
            <Panel>
              <DetailView>
                <Tabs>
                  <SummaryTab />
                  <JsonTab />
                  <RawTab />
                </Tabs>
              </DetailView>
              <ServicesPanel />  {/* slide-out */}
            </Panel>
          </PanelGroup>
          <TagInspectorDrawer />
        </AppShell>
      </QueryClientProvider>
    </Provider>
  </ThemeProvider>
</App>
```

---

## Redux State Shape

```typescript
interface RootState {
  // File management
  files: {
    byId: Record<string, FileState>;
    activeFileId: string | null;
    order: string[];  // for tab ordering
  };

  // Messages for active file
  messages: {
    byId: Record<string, MessageSummary>;
    allIds: string[];
    filteredIds: string[];  // after applying filters
    selectedId: string | null;
    selectedIds: string[];  // for multi-select operations
  };

  // Filters
  filters: {
    global: string;  // command palette query
    msgTypes: string[];
    directions: ('IN' | 'OUT')[];
    sessions: string[];
    presets: FilterPreset[];
    activePreset: string | null;
  };

  // UI state
  ui: {
    tagInspectorOpen: boolean;
    tagInspectorHeight: number;
    servicesPanelOpen: boolean;
    activeDetailTab: 'summary' | 'json' | 'raw';
    theme: 'light' | 'dark' | 'system';
  };

  // Configuration
  config: {
    dictionaries: DictionaryInfo[];
    activeDictionary: string;
    endpoints: EndpointConfig[];
    summaryTags: Record<string, number[]>;  // by msgType
    hyperlinkedTags: number[];
  };
}

interface FileState {
  id: string;
  name: string;
  path: string;
  loadedAt: number;
  messageCount: number;
  dictionary: string;
  hash: string;  // for cache key
}

interface MessageSummary {
  id: string;
  seqNum: number;
  msgType: string;
  msgTypeName: string;
  direction: 'IN' | 'OUT';
  timestamp: string;
  senderCompId: string;
  targetCompId: string;
  // Configurable summary tags
  securityType?: string;
  clOrdId?: string;
  orderId?: string;
  // ... other configured tags
}
```

---

## Backend API Contract

The React app communicates with the .NET backend (cspurefix) via HTTP endpoints.

### Endpoints

```typescript
// POST /api/parse
// Upload a FIX log file, returns message summaries
interface ParseRequest {
  file: File;
  dictionary: string;
  summaryTags?: number[];  // additional tags to extract for summary
}

interface ParseResponse {
  fileId: string;
  messageCount: number;
  messages: MessageSummary[];
  parseErrors?: ParseError[];
}

// GET /api/messages/{fileId}/{messageId}
// Get full message details
interface MessageDetailResponse {
  raw: string;  // raw FIX message
  json: object;  // parsed as JSON (via message factory)
  tags: TagDetail[];
}

interface TagDetail {
  tag: number;
  name: string;
  value: string;
  rawValue: string;
  isEnum: boolean;
  enumDescription?: string;
  component?: string;
  position: number;
}

// GET /api/dictionaries
// List available dictionaries
interface DictionaryListResponse {
  dictionaries: {
    id: string;
    name: string;
    version: string;
    messageTypes: number;
  }[];
}

// GET /api/tag/{tag}
// Get tag metadata
interface TagMetadataResponse {
  tag: number;
  name: string;
  type: string;
  description?: string;
  enumValues?: { value: string; description: string }[];
  components: string[];  // which components contain this tag
}
```

### Service Adapters (Pluggable)

```typescript
// Trade service adapter (optional)
interface ITradeServiceAdapter {
  name: string;
  isAvailable(): Promise<boolean>;
  lookupByTag(tag: number, value: string): Promise<TradeInfo[]>;
}

// Instrument service adapter (optional)
interface IInstrumentServiceAdapter {
  name: string;
  isAvailable(): Promise<boolean>;
  lookupBySymbol(symbol: string): Promise<InstrumentInfo>;
}

// Dev endpoint adapter
interface IDevEndpointAdapter {
  name: string;
  endpoint: string;
  send(messages: string[]): Promise<SendResult>;
  sendSingle(message: string): Promise<SendResult>;
}
```

---

## Feature Details

### 1. File Upload & Caching

```typescript
// Files are cached by content hash
const fileCache = new Map<string, FileState>();

async function uploadFile(file: File): Promise<string> {
  const hash = await computeHash(file);

  if (fileCache.has(hash)) {
    // Switch to cached file
    dispatch(setActiveFile(hash));
    return hash;
  }

  // Upload to backend
  const response = await api.parse({ file, dictionary });

  // Cache locally
  fileCache.set(hash, {
    id: response.fileId,
    name: file.name,
    messages: response.messages,
    // ...
  });

  dispatch(fileLoaded(response));
  return hash;
}
```

### 2. Message Grid Configuration

```typescript
const columns: ColumnDef<MessageSummary>[] = [
  {
    accessorKey: 'timestamp',
    header: 'Time',
    cell: ({ row }) => formatTimestamp(row.original.timestamp),
    size: 100,
  },
  {
    accessorKey: 'direction',
    header: 'Dir',
    cell: ({ row }) => (
      <Badge variant={row.original.direction === 'IN' ? 'blue' : 'green'}>
        {row.original.direction}
      </Badge>
    ),
    size: 50,
  },
  {
    accessorKey: 'msgType',
    header: 'MsgType',
    cell: ({ row }) => (
      <span className={getMsgTypeClass(row.original.msgType)}>
        {row.original.msgTypeName} ({row.original.msgType})
      </span>
    ),
    size: 150,
  },
  // ... additional columns from config
];

// Color coding by message type
function getMsgTypeClass(msgType: string): string {
  const classes: Record<string, string> = {
    'D': 'text-green-500',      // NewOrderSingle
    '8': 'text-blue-500',       // ExecutionReport
    '9': 'text-red-500',        // OrderCancelReject
    'j': 'text-red-600',        // BusinessMessageReject
    '3': 'text-yellow-500',     // Reject
    // ... configurable
  };
  return classes[msgType] || 'text-gray-500';
}
```

### 3. Command Palette Filter Syntax

The command palette supports a precedence-based search:

```typescript
interface FilterQuery {
  // Exact tag=value
  tagFilters: { tag: number; value: string }[];
  // Tag range (show tags 8-35)
  tagRange?: { start: number; end: number };
  // Named filters
  named: ('header' | 'trailer' | 'instrument' | 'enum')[];
  // Free text (searches all values)
  text?: string;
}

function parseQuery(query: string): FilterQuery {
  const result: FilterQuery = { tagFilters: [], named: [] };

  // Match patterns in order of precedence
  const patterns = [
    { regex: /(\d+)=([^\s]+)/g, handler: (m) => result.tagFilters.push({ tag: +m[1], value: m[2] }) },
    { regex: /(\d+)-(\d+)/g, handler: (m) => result.tagRange = { start: +m[1], end: +m[2] } },
    { regex: /\b(header|trailer|instrument|enum)\b/gi, handler: (m) => result.named.push(m[1].toLowerCase()) },
    // Remaining text becomes free search
  ];

  // ... apply patterns
  return result;
}
```

### 4. Tag Inspector

The tag inspector in the bottom drawer shows all tags for the selected message:

```typescript
interface TagInspectorProps {
  tags: TagDetail[];
  filter: string;
  showEnumsOnly: boolean;
  highlightedTag?: number;
}

function TagInspector({ tags, filter, showEnumsOnly }: TagInspectorProps) {
  const filtered = useMemo(() => {
    let result = tags;

    if (showEnumsOnly) {
      result = result.filter(t => t.isEnum);
    }

    if (filter) {
      const query = parseQuery(filter);
      result = applyTagFilter(result, query);
    }

    return result;
  }, [tags, filter, showEnumsOnly]);

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Tag</TableHead>
          <TableHead>Name</TableHead>
          <TableHead>Value</TableHead>
          <TableHead>Component</TableHead>
          <TableHead>Actions</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {filtered.map(tag => (
          <TableRow key={tag.position}>
            <TableCell>{tag.tag}</TableCell>
            <TableCell>{tag.name}</TableCell>
            <TableCell>
              {tag.isEnum ? (
                <span>
                  {tag.value}
                  <span className="text-muted-foreground ml-1">
                    ({tag.enumDescription})
                  </span>
                </span>
              ) : (
                <HyperlinkedValue tag={tag} />
              )}
            </TableCell>
            <TableCell>{tag.component}</TableCell>
            <TableCell>
              <Button size="sm" variant="ghost" onClick={() => showTagInfo(tag.tag)}>
                <InfoIcon />
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
```

### 5. Send to Dev Endpoint

```typescript
interface DevEndpoint {
  name: string;
  url: string;
  method: 'POST' | 'PUT';
  headers?: Record<string, string>;
  transformMessage?: (msg: string) => string;  // e.g., append timestamp to ClOrdID
}

// Configuration
const endpoints: DevEndpoint[] = [
  {
    name: 'Booking Service (UAT)',
    url: 'http://localhost:5000/api/fix/submit',
    method: 'POST',
    transformMessage: (msg) => patchTag(msg, 11, `${getTag(msg, 11)}_${timestamp()}`),
  },
  {
    name: 'Order Gateway (Dev)',
    url: 'http://dev-gateway:8080/orders',
    method: 'POST',
  },
];

// Usage
async function sendToDev(endpoint: DevEndpoint, messages: string[]) {
  const transformed = endpoint.transformMessage
    ? messages.map(endpoint.transformMessage)
    : messages;

  for (const msg of transformed) {
    await fetch(endpoint.url, {
      method: endpoint.method,
      headers: { 'Content-Type': 'text/plain', ...endpoint.headers },
      body: msg,
    });
  }
}
```

### 6. Message Editing

For the "edit and resend" workflow:

```typescript
interface MessageEditor {
  originalMessage: string;
  patches: TagPatch[];
}

interface TagPatch {
  tag: number;
  operation: 'set' | 'append' | 'delete';
  value?: string;
  appendSuffix?: string;  // e.g., "_HHMMSS"
}

// Common patches
const commonPatches: Record<string, TagPatch[]> = {
  'uniqueClOrdId': [
    { tag: 11, operation: 'append', appendSuffix: '_${HHMMSS}' },
  ],
  'uniqueOrderId': [
    { tag: 37, operation: 'append', appendSuffix: '_${HHMMSS}' },
  ],
};
```

---

## Theme Support

Using Tailwind CSS with CSS variables for theming:

```css
/* globals.css */
@tailwind base;
@tailwind components;
@tailwind utilities;

@layer base {
  :root {
    --background: 0 0% 100%;
    --foreground: 222.2 84% 4.9%;
    --card: 0 0% 100%;
    --card-foreground: 222.2 84% 4.9%;
    --primary: 222.2 47.4% 11.2%;
    --primary-foreground: 210 40% 98%;
    /* ... */
  }

  .dark {
    --background: 222.2 84% 4.9%;
    --foreground: 210 40% 98%;
    --card: 222.2 84% 4.9%;
    --card-foreground: 210 40% 98%;
    --primary: 210 40% 98%;
    --primary-foreground: 222.2 47.4% 11.2%;
    /* ... */
  }
}
```

Theme switching via shadcn's theme provider:

```typescript
function ThemeToggle() {
  const { theme, setTheme } = useTheme();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" size="sm">
          {theme === 'dark' ? <MoonIcon /> : <SunIcon />}
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent>
        <DropdownMenuItem onClick={() => setTheme('light')}>Light</DropdownMenuItem>
        <DropdownMenuItem onClick={() => setTheme('dark')}>Dark</DropdownMenuItem>
        <DropdownMenuItem onClick={() => setTheme('system')}>System</DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
```

---

## Configuration File

The app reads configuration from a JSON file:

```json
{
  "dictionaries": [
    { "id": "FIX44", "path": "/dictionaries/FIX44.xml" },
    { "id": "FIX50SP2", "path": "/dictionaries/FIX50SP2.xml" }
  ],
  "defaultDictionary": "FIX44",
  "parserEndpoint": "http://localhost:5001/api",
  "summaryTags": {
    "8": [167, 55],
    "D": [55, 54, 38, 44],
    "default": [55]
  },
  "hyperlinkedTags": [11, 37, 17],
  "colorCoding": {
    "D": { "bg": "bg-green-50 dark:bg-green-900/20", "text": "text-green-700 dark:text-green-300" },
    "8": { "bg": "bg-blue-50 dark:bg-blue-900/20", "text": "text-blue-700 dark:text-blue-300" },
    "9": { "bg": "bg-red-50 dark:bg-red-900/20", "text": "text-red-700 dark:text-red-300" }
  },
  "filterPresets": [
    { "name": "All Trades", "filter": "35=D|8|F|G" },
    { "name": "No Session", "filter": "-35=0|A|1|2|3|4|5" },
    { "name": "Rejects Only", "filter": "35=9|j|3" }
  ],
  "devEndpoints": [
    {
      "name": "Local Booking Service",
      "url": "http://localhost:5000/api/fix/submit",
      "method": "POST"
    }
  ],
  "services": {
    "trade": {
      "enabled": false,
      "endpoint": null
    },
    "instrument": {
      "enabled": false,
      "endpoint": null
    }
  }
}
```

---

## Future: Live Streaming (SignalR)

When adding live streaming later, the architecture supports it:

```typescript
// New slice for live mode
interface LiveState {
  connected: boolean;
  hubUrl: string | null;
  subscriptions: string[];  // session IDs
  buffer: MessageSummary[];  // rolling window
  maxBufferSize: number;
  paused: boolean;
}

// SignalR integration
function useLiveMessages(hubUrl: string, sessionIds: string[]) {
  const connection = useRef<HubConnection>();

  useEffect(() => {
    connection.current = new HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    connection.current.on('MessageReceived', (msg: MessageSummary) => {
      if (!isPaused) {
        dispatch(messageReceived(msg));
      }
    });

    connection.current.start();

    return () => connection.current?.stop();
  }, [hubUrl]);

  // Subscribe to sessions
  useEffect(() => {
    connection.current?.invoke('Subscribe', sessionIds);
  }, [sessionIds]);
}
```

The grid will use virtual scrolling from the start, making the transition to infinite streaming data seamless.

---

## Project Structure

```
fix-viewer/
├── src-tauri/                    # Tauri backend (minimal)
│   ├── src/
│   │   └── main.rs
│   ├── Cargo.toml
│   └── tauri.conf.json
├── src/                          # React frontend
│   ├── components/
│   │   ├── layout/
│   │   │   ├── AppShell.tsx
│   │   │   ├── FileTabs.tsx
│   │   │   ├── Toolbar.tsx
│   │   │   └── TagInspectorDrawer.tsx
│   │   ├── grid/
│   │   │   ├── MessageGrid.tsx
│   │   │   ├── columns.tsx
│   │   │   └── cellRenderers.tsx
│   │   ├── detail/
│   │   │   ├── DetailView.tsx
│   │   │   ├── SummaryTab.tsx
│   │   │   ├── JsonTab.tsx
│   │   │   └── RawTab.tsx
│   │   ├── command/
│   │   │   └── CommandPalette.tsx
│   │   └── ui/                   # shadcn components
│   ├── features/
│   │   ├── files/
│   │   │   ├── filesSlice.ts
│   │   │   └── fileUpload.ts
│   │   ├── messages/
│   │   │   ├── messagesSlice.ts
│   │   │   └── messageApi.ts
│   │   ├── filters/
│   │   │   ├── filtersSlice.ts
│   │   │   └── filterParser.ts
│   │   └── config/
│   │       ├── configSlice.ts
│   │       └── endpoints.ts
│   ├── hooks/
│   │   ├── useMessageSelection.ts
│   │   ├── useDebouncedFilter.ts
│   │   └── useTagInspector.ts
│   ├── lib/
│   │   ├── api.ts
│   │   ├── fixUtils.ts
│   │   └── theme.ts
│   ├── App.tsx
│   ├── main.tsx
│   └── index.css
├── config/
│   └── default.json
├── package.json
├── tailwind.config.js
├── tsconfig.json
└── vite.config.ts
```

---

## Implementation Phases

### Phase 1: Core Viewer (MVP)
- [ ] Tauri + React + Vite setup
- [ ] Basic file upload and parsing (connect to existing backend)
- [ ] Message grid with TanStack Table + virtualization
- [ ] Basic detail view (raw message)
- [ ] Theme support (dark/light)

### Phase 2: Power User Features
- [ ] Command palette with filter syntax
- [ ] Tag inspector drawer
- [ ] Debounced filtering
- [ ] Multi-file caching
- [ ] File tabs

### Phase 3: Advanced Features
- [ ] JSON view (via message factory)
- [ ] Summary tab with configurable tags
- [ ] Hyperlinked tags
- [ ] Row grouping
- [ ] Color coding configuration

### Phase 4: Integration Features
- [ ] Dev endpoint sending
- [ ] Message editing/patching
- [ ] Clipboard operations
- [ ] Export to CSV/JSON

### Phase 5: Live Mode (Future)
- [ ] SignalR connection
- [ ] Real-time message streaming
- [ ] Pause/resume
- [ ] Session subscriptions

---

## Open Questions

1. **Backend hosting** - For Phase 1, is the existing SeeFixServer adequate, or do we need a dedicated viewer backend?

2. **Dictionary bundling** - Should common dictionaries (FIX42, FIX44, FIX50SP2) be bundled in the app, or always fetched from server?

3. **Configuration persistence** - Store in Tauri's app data folder, or allow user-specified config file path?

4. **Multi-window** - Should the app support opening multiple windows (e.g., compare two log files side-by-side)?

---

## Related Documents

- [Message Publisher & Viewer Roadmap](./message-publisher-viewer-roadmap.md)
- [Type Registry Design](./TypeRegistryDesign.md)
