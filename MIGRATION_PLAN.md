# Migration Plan: Phase Out Old Type System

## Current State

### Test Applications (Should Be Demo Apps)
These are NOT unit tests - they are demonstration applications showing how to use the library:

1. **TestAsciiSkeleton** (FIX 4.4)
   - Simple echo-back client/server
   - Shows basic session management, logon, heartbeat
   - Location: `PureFix.Test/Env/Skeleton/`
   - Uses: `PureFix.Types.FIX44.QuickFix.Types` (OLD)

2. **TradeCaptureClient + TradeCaptureServer** (FIX 5.0 SP2)
   - Full bi-directional trade capture system
   - Shows request/response, subscriptions, batching
   - Location: `PureFix.Test/Env/TradeCapture/`
   - Uses: `PureFix.Types.FIX50SP2.QuickFix` (OLD)

### Unit Tests That Use Demo Apps
- `PureFix.Test/Ascii/SessionTest.cs` (7 tests)
  - Uses SkeletonSessionExperiment for E2E session tests
  - Uses TradeCaptureSessionExperiment for trade capture tests

### Type System Dependencies
```
Current (OLD):
├── PureFix.Types (9.9MB, 3,797 files)
│   ├── FIX44.QuickFix.Types (used by Skeleton)
│   └── FIX50SP2.QuickFix (used by TradeCapture)
└── PureFix.Types.Core (31 files, shared interfaces)

New (GENERATED):
├── PureFix.Types.Core (31 files, shared interfaces) ✓
├── PureFix.Types.FIX44 (186 messages, 33K LOC) ✓ READY
└── PureFix.Types.FIX50SP2 (NOT YET GENERATED)
```

## Migration Strategy (Phased Approach)

### Phase 1: Restructure - Extract Demo Applications ⭐ START HERE

**Goal**: Move demonstration apps OUT of test project into proper demo projects

**Actions**:
1. Create new solution folder: `demos/`
2. Create `PureFix.Demo.Skeleton/` console project
   - Move `TestAsciiSkeleton.cs`
   - Move `SkeletonSessionFactory.cs`
   - Move `SkeletonDIContainer.cs`
   - Add configuration files, README
   - Keep using OLD types temporarily

3. Create `PureFix.Demo.TradeCapture/` console project
   - Move `TradeCaptureClient.cs`, `TradeCaptureServer.cs`
   - Move `TradeCaptureSessionFactory.cs`
   - Move `TradeCaptureDIContainer.cs`, `TradeFactory.cs`
   - Add configuration files, README
   - Keep using OLD types temporarily

4. Update `PureFix.Test/Ascii/SessionTest.cs`
   - Add project references to demo projects
   - Keep tests working with demo apps

**Benefits**:
- Clear separation: tests vs. demo applications
- Demo apps can be packaged/distributed separately
- Easier to understand for new users

**Estimate**: 2-4 hours

---

### Phase 2: Generate FIX 5.0 SP2 Types

**Goal**: Create new modular types for FIX 5.0 SP2

**Actions**:
1. Run ModularGenerator on `Data/FIX50SP2.xml`
2. Generate `PureFix.Types.FIX50SP2/` (full specification)
3. Add to solution
4. Build and verify

**Command**:
```csharp
// Add to ModularGeneratorTests.cs
[Test]
[Explicit("Only run when generating full FIX50SP2 types")]
public void Generate_Full_FIX50SP2_Types()
{
    const string dictPath = "../../../../Data/FIX50SP2.xml";
    const string outputPath = "../../../../generated-types";

    var definitions = new FixDefinitions();
    var parser = new QuickFixXmlFileParser(definitions);
    parser.Parse(dictPath);

    var options = ModularGeneratorOptions.FromDictionaryPath(
        dictPath, outputPath, definitions);

    var generator = new ModularGenerator(definitions, options);
    generator.Process();
}
```

**Estimate**: 1-2 hours (mostly waiting for generation + build)

---

### Phase 3: Migrate Skeleton to New FIX44 Types

**Goal**: Convert Skeleton demo to use `PureFix.Types.FIX44` (new system)

**Actions**:
1. Update `PureFix.Demo.Skeleton.csproj`
   - Remove reference to `PureFix.Types`
   - Add reference to `PureFix.Types.FIX44`

2. Update imports in all Skeleton files:
   ```csharp
   // OLD:
   using PureFix.Types.FIX44.QuickFix.Types;

   // NEW:
   using PureFix.Types.FIX44;
   using PureFix.Types.FIX44.Components;
   ```

3. Update code for nested group syntax:
   ```csharp
   // OLD (if any groups used):
   var parties = new ExecutionReportNoPartyIDs();

   // NEW:
   var parties = new Parties.NoPartyIDs();
   ```

4. Test E2E scenarios still work

**Estimate**: 2-4 hours (depends on code changes needed)

---

### Phase 4: Migrate TradeCapture to New FIX50SP2 Types

**Goal**: Convert TradeCapture demo to use `PureFix.Types.FIX50SP2` (new system)

**Actions**: Same as Phase 3, but for TradeCapture
1. Update project references
2. Update imports
3. Update nested group syntax
4. Test E2E scenarios

**Estimate**: 3-6 hours (more complex app)

---

### Phase 5: Verify All Tests Pass with New Types

**Goal**: Ensure entire test suite works with new type system

**Actions**:
1. Run full test suite:
   ```bash
   dotnet test PureFix.Test/PureFix.Test.csproj
   dotnet test PureFix.Test.ModularTypes/PureFix.Test.ModularTypes.csproj
   ```

2. Fix any issues that arise
3. Verify demo apps work standalone

**Estimate**: 2-4 hours

---

### Phase 6: Remove Old PureFix.Types (The Big Cleanup)

**Goal**: Delete legacy 9.9MB monolithic type assembly

**Actions**:
1. Search entire solution for references to old types:
   ```bash
   grep -r "PureFix.Types.FIX44.QuickFix" .
   grep -r "PureFix.Types.FIX50SP2.QuickFix" .
   ```

2. Verify NO code references old types (except in git history)

3. Remove from solution:
   ```bash
   dotnet sln remove PureFix.Types/PureFix.Types.csproj
   ```

4. Delete directory:
   ```bash
   rm -rf PureFix.Types/
   ```

5. Update all documentation/README files

6. Commit with clear message about breaking change

**Estimate**: 1-2 hours

---

### Phase 7: Package for Distribution (Future)

**Goal**: Prepare new type system for NuGet distribution

**Actions**:
1. Add NuGet package metadata to:
   - `PureFix.Types.Core.csproj`
   - `PureFix.Types.FIX44.csproj`
   - `PureFix.Types.FIX50SP2.csproj`

2. Create release notes documenting breaking changes

3. Version as 2.0.0 (major breaking change)

4. Publish to NuGet

**Estimate**: 2-4 hours

---

## Total Estimate

**Minimum**: ~13 hours
**Maximum**: ~26 hours
**Realistic**: ~16-20 hours over 1-2 weeks

## Risks & Mitigation

| Risk | Mitigation |
|------|------------|
| Breaking changes in production code | Thorough testing in each phase |
| Demo apps don't work with new types | Keep old types until migration complete |
| Users depend on old type structure | Document migration path, provide compatibility guide |
| FIX50SP2 generation has issues | Test generation on smaller dictionary first |

## Success Criteria

✅ Demo apps moved to `demos/` folder
✅ Both demo apps use new type system
✅ All 255 legacy tests still pass
✅ All 31 modular tests still pass
✅ Old `PureFix.Types/` directory deleted
✅ Solution builds with 0 errors
✅ Demo apps run standalone

## Decision Points

**DECISION NEEDED**: Should we do this migration now, or wait?
- ✅ **DO NOW**: We have validated new types work (286 tests pass)
- ✅ **DO NOW**: Demo apps clearly separated from tests is better architecture
- ⚠️ **WAIT**: If you need old types for production systems still in use

**Recommendation**: Start with Phase 1 (extract demo apps) as it's low risk and improves structure regardless of type system.
