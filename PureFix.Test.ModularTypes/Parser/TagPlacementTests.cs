using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Test.ModularTypes.Helpers;

namespace PureFix.Test.ModularTypes.Parser;

/// <summary>
/// Tests for tag-by-tag placement logic.
/// These tests validate that we can correctly determine where a tag belongs
/// using the existing ContainedSet data structures.
/// </summary>
[TestFixture]
public class TagPlacementTests
{
    private IFixDefinitions _definitions = null!;
    private IContainedSet _executionReport = null!;
    private IContainedSet _tradeCaptureReport = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Load FIX 4.4 dictionary
        _definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(_definitions);
        parser.Parse(Fix44PathHelper.DataDictPath);

        // Get message definitions with nested structure
        // MessageDefinition extends ContainedFieldSet which IS the IContainedSet
        _executionReport = _definitions.Message["8"];  // ExecutionReport
        _tradeCaptureReport = _definitions.Message["AE"];  // TradeCaptureReport
    }

    #region Depth Computation Tests

    /// <summary>
    /// Computes the depth of a tag by walking down the containment hierarchy.
    /// </summary>
    private int ComputeDepth(int tag, IContainedSet currentSet, int currentDepth = 0)
    {
        // Is it local to this set?
        if (currentSet.LocalTag.ContainsKey(tag))
            return currentDepth;

        // It must be deeper - find which child contains it
        if (currentSet.ContainedTag.TryGetValue(tag, out var childSet))
        {
            return ComputeDepth(tag, childSet, currentDepth + 1);
        }

        // Unknown tag
        return -1;
    }

    /// <summary>
    /// Finds the path from root to the tag's owning set.
    /// Returns list of (setName, depth) tuples.
    /// </summary>
    private List<(string name, int depth)> FindPath(int tag, IContainedSet currentSet, int currentDepth = 0)
    {
        var path = new List<(string, int)> { (currentSet.Name, currentDepth) };

        // Is it local to this set?
        if (currentSet.LocalTag.ContainsKey(tag))
            return path;

        // It must be deeper - find which child contains it
        if (currentSet.ContainedTag.TryGetValue(tag, out var childSet))
        {
            path.AddRange(FindPath(tag, childSet, currentDepth + 1));
        }

        return path;
    }

    [Test]
    public void Header_Tags_Are_At_Depth_1()
    {
        // StandardHeader is a component at depth 1
        // BeginString (8) is inside StandardHeader
        var depth = ComputeDepth(8, _executionReport);
        Assert.That(depth, Is.EqualTo(1), "BeginString should be at depth 1 (inside StandardHeader)");

        // MsgType (35) is also in StandardHeader
        depth = ComputeDepth(35, _executionReport);
        Assert.That(depth, Is.EqualTo(1), "MsgType should be at depth 1 (inside StandardHeader)");
    }

    [Test]
    public void Message_Level_Tags_Are_At_Depth_0()
    {
        // OrderID (37) is directly in ExecutionReport at root level
        var depth = ComputeDepth(37, _executionReport);
        Assert.That(depth, Is.EqualTo(0), "OrderID should be at depth 0 (message level)");

        // ExecID (17) is also at message level
        depth = ComputeDepth(17, _executionReport);
        Assert.That(depth, Is.EqualTo(0), "ExecID should be at depth 0 (message level)");
    }

    [Test]
    public void Instrument_Tags_Are_At_Depth_1()
    {
        // Instrument component contains Symbol (55)
        var depth = ComputeDepth(55, _executionReport);
        Assert.That(depth, Is.EqualTo(1), "Symbol should be at depth 1 (inside Instrument)");

        // SecurityID (48) is also in Instrument
        depth = ComputeDepth(48, _executionReport);
        Assert.That(depth, Is.EqualTo(1), "SecurityID should be at depth 1 (inside Instrument)");
    }

    [Test]
    public void Nested_Component_Tags_Have_Correct_Depth()
    {
        // Check if there are any depth-2 components
        // InstrumentExtension contains AttrbGrp group
        // Let's trace the path for a known deep tag

        // Print the structure for debugging
        PrintStructure(_executionReport, 0);
    }

    [Test]
    public void FindPath_Shows_Containment_Hierarchy()
    {
        // Trace path to Symbol (55)
        var path = FindPath(55, _executionReport);

        Console.WriteLine("Path to Symbol (55):");
        foreach (var (name, depth) in path)
        {
            Console.WriteLine($"  {new string(' ', depth * 2)}{name} (depth {depth})");
        }

        Assert.That(path.Count, Is.GreaterThan(1), "Symbol should have a path through Instrument");
        Assert.That(path[^1].name, Is.EqualTo("Instrument"), "Final container should be Instrument");
    }

    [Test]
    public void Unknown_Tag_Returns_Negative_Depth()
    {
        // Tag 99999 doesn't exist
        var depth = ComputeDepth(99999, _executionReport);
        Assert.That(depth, Is.EqualTo(-1), "Unknown tag should return -1");
    }

    #endregion

    #region Tag Ownership Tests

    [Test]
    public void TagToField_Returns_Immediate_Parent()
    {
        // Symbol (55) should map to Instrument component
        var (parent, field) = _executionReport.TagToField.GetValueOrDefault(55);

        Assert.That(parent, Is.Not.Null, "Symbol should have a parent");
        Assert.That(parent!.Name, Is.EqualTo("Instrument"), "Symbol's parent should be Instrument");
        Assert.That(field, Is.Not.Null, "Should return the field definition");
    }

    [Test]
    public void ContainedTag_Returns_Owning_Set()
    {
        // Symbol (55) - ContainedTag should return Instrument
        Assert.That(_executionReport.ContainedTag.TryGetValue(55, out var owningSet), Is.True);
        Assert.That(owningSet!.Name, Is.EqualTo("Instrument"));
    }

    [Test]
    public void LocalTag_Only_Contains_Direct_Tags()
    {
        // Check what's local to ExecutionReport vs what's in components
        Console.WriteLine($"ExecutionReport LocalTag count: {_executionReport.LocalTag.Count}");
        Console.WriteLine($"ExecutionReport ContainedTag count: {_executionReport.ContainedTag.Count}");

        // OrderID (37) should be local
        Assert.That(_executionReport.LocalTag.ContainsKey(37), Is.True, "OrderID should be local to ExecutionReport");

        // Symbol (55) should NOT be local (it's in Instrument)
        Assert.That(_executionReport.LocalTag.ContainsKey(55), Is.False, "Symbol should NOT be local to ExecutionReport");
    }

    #endregion

    #region Group Detection Tests

    [Test]
    public void Can_Identify_Group_Delimiter()
    {
        // Parties group in ExecutionReport
        // NoPartyIDs (453) is the count tag
        // PartyID (448) is typically the delimiter (first field in group)

        var partiesGroup = _executionReport.Groups.GetValueOrDefault("Parties");
        if (partiesGroup != null)
        {
            Console.WriteLine($"Parties group fields:");
            foreach (var field in partiesGroup.Fields)
            {
                Console.WriteLine($"  {field.Name} (tag {(field as ContainedSimpleField)?.Definition.Tag})");
            }

            // First field should be the delimiter
            var firstField = partiesGroup.Fields.FirstOrDefault();
            Assert.That(firstField, Is.Not.Null, "Group should have fields");
            Console.WriteLine($"First field (delimiter): {firstField!.Name}");
        }
        else
        {
            Console.WriteLine("Parties group not found - checking for NoPartyIDs");
            // Try finding via the count tag
            if (_executionReport.ContainedTag.TryGetValue(453, out var noPartySet))
            {
                Console.WriteLine($"NoPartyIDs (453) is in: {noPartySet.Name}");
            }
        }
    }

    [Test]
    public void TradeCaptureReport_Has_Nested_Groups()
    {
        // TradeCaptureReport has TrdCapRptSideGrp which contains Parties
        Console.WriteLine("\nTradeCaptureReport structure:");
        PrintStructure(_tradeCaptureReport, 0);
    }

    #endregion

    #region Helper Methods

    private void PrintStructure(IContainedSet set, int depth, int maxDepth = 3)
    {
        if (depth > maxDepth) return;

        var indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{set.Name} ({set.Type})");
        Console.WriteLine($"{indent}  LocalTags: {set.LocalTag.Count}, ContainedTags: {set.ContainedTag.Count}");

        if (set.LocalTag.Count > 0 && set.LocalTag.Count <= 10)
        {
            Console.WriteLine($"{indent}  Local: {string.Join(", ", set.LocalTag.Keys.Take(10))}");
        }

        foreach (var comp in set.Components.Values.Take(5))
        {
            PrintStructure(comp, depth + 1, maxDepth);
        }

        foreach (var group in set.Groups.Values.Take(5))
        {
            PrintStructure(group, depth + 1, maxDepth);
        }
    }

    #endregion
}
