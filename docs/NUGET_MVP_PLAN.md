# NuGet MVP Plan: Logging, Configuration & Example Application

This document focuses on the minimum work needed to publish PureFix on NuGet with a usable example (TradeCapture).

## Current State Assessment

### Logging
- **Interface**: `ILogger` and `ILogFactory` in `PureFix.Types.Core`
- **Implementation**: `ConsoleLogFactory` in `PureFix.App` using hardcoded Serilog
- **Pattern**: Two-logger pattern (app events + raw FIX messages)
- **Problem**: No configuration, Serilog is baked in

### Message Store
- **Interface**: `IFixSessionStore` and `IFixSessionStoreFactory`
- **Implementations**: `FileSessionStore` (QuickFix-compatible), `MemorySessionStore`
- **Status**: Well designed, factory pattern already in place
- **Problem**: Not configurable via JSON config

### Configuration
- **Format**: JSON session config files
- **Content**: Session details, TCP, TLS settings
- **Missing**: Log factory configuration, store factory configuration

---

## Question 1: Logging Strategy

### Option A: Keep Custom Interface + Serilog (Recommended)

Keep `ILogFactory`/`ILogger` but make Serilog configurable via `serilog.json`:

**Pros:**
- Minimal code changes
- Serilog is mature and widely used
- Already have `serilog.json` example
- Serilog supports file, console, seq, elasticsearch, etc.

**Cons:**
- Library dictates Serilog dependency
- Users who prefer NLog/log4net must adapt

**Implementation:**
```csharp
// In PureFix.App or new PureFix.Logging.Serilog package
public class SerilogLogFactory : ILogFactory
{
    public SerilogLogFactory(IConfiguration configuration)
    {
        // Read from configuration (appsettings.json or serilog.json)
        _appLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}
```

### Option B: Microsoft.Extensions.Logging Abstraction

Switch to `Microsoft.Extensions.Logging.Abstractions`:

**Pros:**
- Industry standard for .NET
- Zero opinion on logging provider
- Users can use any provider (Serilog, NLog, etc.)
- Works seamlessly with ASP.NET Core

**Cons:**
- Breaking change to existing interface
- Lose the two-logger pattern (app vs FIX)
- More complex for users to configure

**Implementation:**
```csharp
// Replace ILogFactory with ILoggerFactory
public class FixSession
{
    private readonly ILogger<FixSession> _logger;

    public FixSession(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<FixSession>();
    }
}
```

### Option C: Hybrid Approach

Keep `ILogFactory` interface but provide adapters:

**Pros:**
- Non-breaking for existing code
- Users can choose provider
- Preserves two-logger pattern

**Implementation:**
```csharp
// Adapter from Microsoft.Extensions.Logging to PureFix ILogger
public class MicrosoftLogFactory : ILogFactory
{
    private readonly ILoggerFactory _factory;

    public MicrosoftLogFactory(ILoggerFactory factory) => _factory = factory;

    public Types.ILogger MakeLogger(string name)
        => new MicrosoftLoggerAdapter(_factory.CreateLogger(name));
}
```

### Recommendation

**Option A (Serilog) for MVP**, with interface preserved for future Option C.

Rationale:
1. Serilog is the de-facto standard for .NET logging
2. Configuration via JSON is well documented
3. Minimal changes required
4. The two-logger pattern (app + FIX) is valuable for FIX engines
5. Can add Microsoft.Extensions.Logging adapter later as Option C

---

## Question 2: Configurable Log Factory via JSON

### Proposed Configuration Schema

Extend session config or use separate logging config:

```json
{
  "application": {
    "name": "TradeCapture",
    "type": "initiator",
    "tcp": { "host": "localhost", "port": 2344 },
    "dictionary": "FIX50SP2.xml"
  },

  "logging": {
    "provider": "serilog",
    "appLog": {
      "console": true,
      "file": {
        "path": "logs/app-{Date}.log",
        "rollingInterval": "Day",
        "retainedFileCountLimit": 21,
        "fileSizeLimitBytes": 524288000
      }
    },
    "fixLog": {
      "console": false,
      "file": {
        "path": "logs/fix-{Date}.log",
        "rollingInterval": "Day"
      }
    },
    "minimumLevel": "Information"
  },

  "store": {
    "type": "file",
    "directory": "store"
  },

  "SenderCompId": "SENDER",
  "TargetCompID": "TARGET",
  "BeginString": "FIX.5.0SP2"
}
```

### Implementation Steps

1. **Add logging configuration types:**
```csharp
// PureFix.Types/Config/LoggingConfig.cs
public class LoggingConfig
{
    public string Provider { get; set; } = "serilog";
    public LogOutputConfig AppLog { get; set; } = new();
    public LogOutputConfig FixLog { get; set; } = new();
    public string MinimumLevel { get; set; } = "Information";
}

public class LogOutputConfig
{
    public bool Console { get; set; } = true;
    public FileLogConfig? File { get; set; }
}

public class FileLogConfig
{
    public string Path { get; set; } = "logs/app.log";
    public string RollingInterval { get; set; } = "Day";
    public int RetainedFileCountLimit { get; set; } = 21;
    public long FileSizeLimitBytes { get; set; } = 524288000;
}
```

2. **Add store configuration:**
```csharp
// PureFix.Types/Config/StoreConfig.cs
public class StoreConfig
{
    public string Type { get; set; } = "memory"; // "memory" or "file"
    public string? Directory { get; set; }
}
```

3. **Extend SessionDescription:**
```csharp
public class SessionDescription : ISessionDescription
{
    // Existing properties...
    public LoggingConfig? Logging { get; set; }
    public StoreConfig? Store { get; set; }
}
```

4. **Create configurable log factory:**
```csharp
// PureFix.App/ConfigurableLogFactory.cs
public class ConfigurableLogFactory : ILogFactory
{
    private readonly Serilog.ILogger _appLogger;
    private readonly Serilog.ILogger _plainLogger;

    public ConfigurableLogFactory(LoggingConfig config)
    {
        _appLogger = BuildLogger(config.AppLog, config.MinimumLevel, withMetadata: true);
        _plainLogger = BuildLogger(config.FixLog, config.MinimumLevel, withMetadata: false);
    }

    private static Serilog.ILogger BuildLogger(LogOutputConfig config, string level, bool withMetadata)
    {
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Is(ParseLevel(level));

        var template = withMetadata
            ? "[{Timestamp:HH:mm:ss.fff}] [{Level:u3}] [{ThreadId}] {Message}{NewLine}{Exception}"
            : "{Message}{NewLine}";

        if (config.Console)
            logConfig.WriteTo.Console(outputTemplate: template);

        if (config.File != null)
        {
            logConfig.WriteTo.File(
                config.File.Path,
                rollingInterval: ParseInterval(config.File.RollingInterval),
                retainedFileCountLimit: config.File.RetainedFileCountLimit,
                fileSizeLimitBytes: config.File.FileSizeLimitBytes,
                outputTemplate: template);
        }

        if (withMetadata)
            logConfig.Enrich.WithThreadId();

        return logConfig.CreateLogger();
    }
}
```

5. **Update FixConfig to auto-create factories:**
```csharp
public static IFixConfig MakeConfigFromPaths(string dictionaryPath, string sessionPath)
{
    // ... existing parsing ...

    var config = new FixConfig
    {
        Definitions = definitions,
        Description = sessionDescription,
    };

    // Auto-create log factory from config
    if (sessionDescription?.Logging != null)
    {
        config.LogFactory = new ConfigurableLogFactory(sessionDescription.Logging);
    }

    // Auto-create store factory from config
    if (sessionDescription?.Store != null)
    {
        config.SessionStoreFactory = sessionDescription.Store.Type switch
        {
            "file" => new FileSessionStoreFactory(sessionDescription.Store.Directory ?? "store"),
            _ => new MemorySessionStoreFactory()
        };
    }

    return config;
}
```

---

## Question 3: Message Store Configuration

The store is already well-designed with the factory pattern. We just need to make it configurable via JSON.

### Changes Needed

1. Add `Store` property to `SessionDescription` (shown above)
2. Auto-create store factory in `FixConfig.MakeConfigFromPaths()`
3. Document the configuration options

### Future Extensibility

The `IFixSessionStoreFactory` interface allows custom implementations:
- SQL database store
- Redis store
- S3/blob storage
- Custom QuickFix-compatible stores

Users would implement `IFixSessionStoreFactory` and set it on config:
```csharp
config.SessionStoreFactory = new MyRedisSessionStoreFactory(connectionString);
```

---

## Implementation Plan for NuGet MVP

### Phase 1: Configuration Infrastructure - COMPLETED

**Task 1.1: Add configuration types** - DONE
- Added `LoggingConfig`, `StoreConfig` classes to `PureFix.Types/Config`
- Added `Logging` and `Store` properties to `ISessionDescription` and `SessionDescription`
- JSON deserialization works automatically

**Task 1.2: Create ConfigurableLogFactory** - DONE
- Created `ConfigurableLogFactory` in `PureFix.App`
- Updated `ConsoleLogFactory` in `Examples.Shared` to use Serilog and read from `LoggingConfig`
- Supports console + file output with separate app/FIX log configuration
- Backward-compatible constructors for existing code

**Task 1.3: Update FixConfig creation** - DONE
- `FixConfig.MakeConfigFromPaths()` now auto-creates `IFixSessionStoreFactory` from config
- Log factory creation happens at application level (reads from `config.Description.Logging`)
- Defaults to memory store if not specified

### Phase 2: Example Application (TradeCapture)

**Task 2.1: Create standalone example**
- Move `TradeCapture` to use new config-driven approach
- Single JSON config file with logging + store settings
- Clear README showing how to configure

**Task 2.2: Example configuration file**
```json
{
  "application": {
    "name": "TradeCaptureClient",
    "type": "initiator",
    "tcp": { "host": "broker.example.com", "port": 9880 },
    "dictionary": "FIX50SP2.xml"
  },
  "logging": {
    "appLog": { "console": true, "file": { "path": "logs/app.log" } },
    "fixLog": { "console": false, "file": { "path": "logs/fix.log" } }
  },
  "store": {
    "type": "file",
    "directory": "sessions"
  },
  "SenderCompId": "MYCLIENT",
  "TargetCompID": "BROKER",
  "BeginString": "FIX.5.0SP2",
  "Username": "user",
  "Password": "pass"
}
```

### Phase 3: NuGet Packaging

**Task 3.1: Package structure**
```
PureFix.Types.Core        - Interfaces (ILogger, ILogFactory, IFixMessage, etc.)
PureFix.Types             - Config classes, JSON helpers
PureFix.Transport         - Session, store, transport layer
PureFix.App               - Serilog-based log factory, app hosting
PureFix.Types.FIX50SP2    - Generated FIX 5.0 SP2 types (example)
```

**Task 3.2: NuGet metadata**
- README for NuGet page
- License (MIT)
- Tags, description
- Repository URL

### Phase 4: Post-NuGet (Separate GitHub Repos)

**Task 4.1: Example as standalone repo**
- `github.com/yourname/purefix-example-tradecapture`
- References PureFix NuGet packages
- Complete working example
- Docker support (optional)

**Task 4.2: Message viewer (future)**
- Upload FIX log files
- Parse based on registry (SenderCompID -> dictionary mapping)
- React GUI for viewing
- Containerized deployment

---

## Summary

| Question | Answer |
|----------|--------|
| **1. Logging approach** | Keep custom `ILogFactory` with Serilog as default provider. Configurable via JSON. Can add Microsoft.Extensions.Logging adapter later. |
| **2. Log factory config** | Add `LoggingConfig` to session JSON. Auto-create factory in `FixConfig`. Separate app log and FIX log configuration. |
| **3. Store config** | Add `StoreConfig` to session JSON. Auto-create factory in `FixConfig`. Support `memory` and `file` types. |

## Files to Change

| File | Change |
|------|--------|
| `PureFix.Types/Config/LoggingConfig.cs` | New - config classes |
| `PureFix.Types/Config/StoreConfig.cs` | New - config class |
| `PureFix.Types/Config/SessionDescription.cs` | Add Logging, Store properties |
| `PureFix.App/ConfigurableLogFactory.cs` | New - config-driven Serilog factory |
| `PureFix.Transport/FixConfig.cs` | Auto-create factories from config |
| `Examples/PureFix.Examples.TradeCapture/` | Update to use config-driven approach |

## Effort Estimate

- Phase 1: Configuration Infrastructure - Small
- Phase 2: Example Application - Small
- Phase 3: NuGet Packaging - Small
- Phase 4: Post-NuGet - Medium (separate effort)

---

_Created: December 2025_
