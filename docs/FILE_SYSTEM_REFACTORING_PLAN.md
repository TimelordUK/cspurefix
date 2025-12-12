# File System Refactoring Plan

## Overview

This document outlines the plan to refactor the cspurefix file system to align project folders logically, particularly around core types and auto-generated types.

## Current Structure Issues

| Folder | Contents | Issue |
|--------|----------|-------|
| `PureFix.Types.Core/` | Interfaces, attributes, base classes, metrics, structs | Well-organized ✓ |
| `PureFix.Types/` | ElasticBuffer, Tags, MsgType, Registry, Config, Arrow, Converters | Mixed - contains items that should be in Core |
| `generated-types/` | FIX44, FIX44UnitTest, FIX50SP2 | Breaks `PureFix.*` naming convention |
| `Data/` | XML dictionaries + PureFix.Data.csproj | OK but path resolution was fragile |

## Refactoring Phases

### Phase 1: Path Helper Changes ✅ COMPLETED

**Commit:** `be4088e0` - Use AppDomain.BaseDirectory for reliable test path resolution

**Changes Made:**
- Replace `Directory.GetCurrentDirectory()` with `AppDomain.CurrentDomain.BaseDirectory`
- Affects: `PureFix.Benchmarks/Fix44PathHelper.cs`, `PureFix.Test.ModularTypes/Helpers/Fix44PathHelper.cs`
- Purpose: Ensure paths resolve correctly regardless of working directory (IDE, CI/CD, etc.)

---

### Phase 2: Consolidate Core Types (PARTIAL - Key types moved)

**Goal:** Move remaining "core" types from `PureFix.Types` to `PureFix.Types.Core`

**Files Moved (✅ COMPLETED):**

| File | Target Location | Status |
|------|-----------------|--------|
| `TagPos.cs` | `PureFix.Types.Core/Structs/TagPos.cs` | ✅ Moved |
| `Tags.cs` | `PureFix.Types.Core/Tags.cs` | ✅ Moved |
| `MsgType.cs` | `PureFix.Types.Core/MsgType.cs` | ✅ Moved |

**Files Still To Consider Moving:**

| File | Target Location | Rationale |
|------|-----------------|-----------|
| `ElasticBuffer.cs` | `PureFix.Types.Core/Buffer/` | Core buffer type used everywhere |
| `ElasticBuffer.DateTime.cs` | `PureFix.Types.Core/Buffer/` | Extension of ElasticBuffer |
| `TagManager.cs` | `PureFix.Types.Core/` | Core tag management |
| `TagMetaData.cs` | `PureFix.Types.Core/` | Core tag metadata |
| `SessionRejectReason.cs` | `PureFix.Types.Core/Enums/` | Core enum |
| `QuickLookup.cs` | `PureFix.Types.Core/` | Core lookup utility |
| `Extensions.cs` | `PureFix.Types.Core/` | Core extensions |

**Files to Keep in `PureFix.Types/`:**

| File | Rationale |
|------|-----------|
| `Config/` | Application-level configuration |
| `Registry/` | Type registry for runtime |
| `Arrow/` | Arrow format support (optional feature) |
| `Converters/` | JSON/other converters (if not duplicated in Core) |
| `DefaultFixWriter.cs` | Default implementation |
| `FixVersionParser.cs` | Version parsing utility |
| `FixDefinitionSource.cs` | Definition source enum |
| `IFixMerge.cs` | Merge interface |
| `JsonHelper.cs` | JSON utilities |
| `RealtimeClock.cs` | Clock implementation |

**Steps:**
1. Create new folders in `PureFix.Types.Core/` as needed (e.g., `Buffer/`)
2. Move files one by one, updating namespaces
3. Update all references across the solution
4. Run tests after each move to ensure nothing breaks
5. Update `PureFix.Types.csproj` and `PureFix.Types.Core.csproj` as needed

---

### Phase 3: Rename Generated Types Folder

**Goal:** Rename `generated-types/` to follow `PureFix.*` naming convention

**Options:**
- Option A: `PureFix.Generated/` (contains FIX44, FIX50SP2 subfolders)
- Option B: Move to root level as `PureFix.Types.FIX44/`, `PureFix.Types.FIX50SP2/`
- Option C: Keep folder but rename to `PureFix.Generated.Types/`

**Recommended:** Option B - Move generated projects to root level alongside other `PureFix.*` projects

**Changes Required:**
1. Move `generated-types/PureFix.Types.FIX44/` to `PureFix.Types.FIX44/`
2. Move `generated-types/PureFix.Types.FIX44UnitTest/` to `PureFix.Types.FIX44UnitTest/`
3. Move `generated-types/PureFix.Types.FIX50SP2/` to `PureFix.Types.FIX50SP2/`
4. Update solution file references
5. Update any path helpers or scripts that reference `generated-types/`
6. Delete empty `generated-types/` folder

---

### Phase 4: Test Data Path Organization

**Goal:** Make test data paths more maintainable

**Current Issue:**
```csharp
public static readonly string DataDictRootPath = Path.Join(BaseDir, "..", "..", "..", "..", "Data");
```

**Proposed Solutions:**
1. Copy test data to test project output directory during build
2. Use embedded resources for small test files
3. Create a shared test data project that copies files to output

**Recommended:** Add `<Content>` items to test csproj that copy required data files to output directory, eliminating relative path traversal.

---

## Verification Checklist

After each phase:
- [ ] `dotnet build` succeeds with no new errors
- [ ] `dotnet test` - all 374 tests pass
- [ ] Benchmarks still run correctly
- [ ] Example projects still compile and run

---

## Notes

- Phase 1 was completed to establish reliable path resolution before moving files
- Each phase should be a separate commit for easy rollback
- Run full test suite after each file move
- Update `claude.md` project documentation after major changes
