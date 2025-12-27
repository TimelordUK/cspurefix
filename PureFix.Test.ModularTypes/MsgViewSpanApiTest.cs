using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Text;
using PureFix.Types;
using PureFix.Test.ModularTypes.Helpers;

namespace PureFix.Test.ModularTypes
{
    /// <summary>
    /// Tests for the zero-allocation span-based MsgView API.
    /// </summary>
    public class MsgViewSpanApiTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.LogonReplayPath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        // ===== GetSpan Tests =====

        [Test]
        public void GetSpan_Returns_Raw_Bytes_For_Tag()
        {
            var view = _views[0];
            var span = view.GetSpan(35); // MsgType
            Assert.That(span.Length, Is.EqualTo(1));
            Assert.That(span[0], Is.EqualTo((byte)'A'));
        }

        [Test]
        public void GetSpan_Returns_Empty_For_Missing_Tag()
        {
            var view = _views[0];
            var span = view.GetSpan(9999);
            Assert.That(span.IsEmpty, Is.True);
        }

        [Test]
        public void TryGetSpan_Returns_True_When_Tag_Found()
        {
            var view = _views[0];
            var found = view.TryGetSpan(35, out var span);
            Assert.That(found, Is.True);
            Assert.That(span.Length, Is.EqualTo(1));
            Assert.That(span[0], Is.EqualTo((byte)'A'));
        }

        [Test]
        public void TryGetSpan_Returns_False_When_Tag_Missing()
        {
            var view = _views[0];
            var found = view.TryGetSpan(9999, out var span);
            Assert.That(found, Is.False);
            Assert.That(span.IsEmpty, Is.True);
        }

        // ===== IsTagEqual Tests =====

        [Test]
        public void IsTagEqual_With_Bytes_Returns_True_For_Match()
        {
            var view = _views[0];
            var result = view.IsTagEqual(35, "A"u8);
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsTagEqual_With_Bytes_Returns_False_For_Mismatch()
        {
            var view = _views[0];
            var result = view.IsTagEqual(35, "B"u8);
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsTagEqual_With_Bytes_Returns_False_For_Missing_Tag()
        {
            var view = _views[0];
            var result = view.IsTagEqual(9999, "A"u8);
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsTagEqual_With_String_Returns_True_For_Match()
        {
            var view = _views[0];
            var result = view.IsTagEqual(35, "A");
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsTagEqual_With_String_Returns_False_For_Mismatch()
        {
            var view = _views[0];
            var result = view.IsTagEqual(35, "B");
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsTagEqual_BeginString_Matches()
        {
            var view = _views[0];
            var result = view.IsTagEqual(8, "FIX4.4"u8);
            Assert.That(result, Is.True);
        }

        // ===== TagStartsWith Tests =====

        [Test]
        public void TagStartsWith_Returns_True_For_Prefix_Match()
        {
            var view = _views[0];
            var result = view.TagStartsWith(8, "FIX"u8);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TagStartsWith_Returns_False_For_No_Match()
        {
            var view = _views[0];
            var result = view.TagStartsWith(8, "FIXT"u8);
            Assert.That(result, Is.False);
        }

        [Test]
        public void TagStartsWith_Returns_False_For_Missing_Tag()
        {
            var view = _views[0];
            var result = view.TagStartsWith(9999, "FIX"u8);
            Assert.That(result, Is.False);
        }

        // ===== MatchTag Tests =====

        [Test]
        public void MatchTag_Returns_Index_Of_Matching_Value()
        {
            var view = _views[0];
            // MsgType is "A" (Logon)
            var idx = view.MatchTag(35, "0"u8, "A"u8); // Heartbeat, Logon
            Assert.That(idx, Is.EqualTo(1)); // "A" is at index 1
        }

        [Test]
        public void MatchTag_Returns_Minus1_When_No_Match()
        {
            var view = _views[0];
            var idx = view.MatchTag(35, "0"u8, "1"u8); // Heartbeat, TestRequest
            Assert.That(idx, Is.EqualTo(-1));
        }

        [Test]
        public void MatchTag_Returns_Minus1_For_Missing_Tag()
        {
            var view = _views[0];
            var idx = view.MatchTag(9999, "A"u8, "B"u8);
            Assert.That(idx, Is.EqualTo(-1));
        }

        [Test]
        public void MatchTag_Three_Values_Works()
        {
            var view = _views[0];
            var idx = view.MatchTag(35, "0"u8, "1"u8, "A"u8);
            Assert.That(idx, Is.EqualTo(2)); // "A" is at index 2
        }

        [Test]
        public void MatchTag_Four_Values_Works()
        {
            var view = _views[0];
            var idx = view.MatchTag(35, "0"u8, "1"u8, "5"u8, "A"u8);
            Assert.That(idx, Is.EqualTo(3)); // "A" is at index 3
        }

        // ===== TryGet Pattern Tests =====

        [Test]
        public void TryGetInt32_Returns_True_And_Value_When_Found()
        {
            var view = _views[0];
            var found = view.TryGetInt32(9, out var value); // BodyLength
            Assert.Multiple(() =>
            {
                Assert.That(found, Is.True);
                Assert.That(value, Is.EqualTo(208));
            });
        }

        [Test]
        public void TryGetInt32_Returns_False_When_Missing()
        {
            var view = _views[0];
            var found = view.TryGetInt32(9999, out var value);
            Assert.Multiple(() =>
            {
                Assert.That(found, Is.False);
                Assert.That(value, Is.EqualTo(0));
            });
        }

        [Test]
        public void TryGetInt64_Returns_True_And_Value_When_Found()
        {
            var view = _views[0];
            var found = view.TryGetInt64(9, out var value);
            Assert.Multiple(() =>
            {
                Assert.That(found, Is.True);
                Assert.That(value, Is.EqualTo(208L));
            });
        }

        [Test]
        public void TryGetBool_Returns_True_And_Value_When_Found()
        {
            var view = _views[0];
            var found = view.TryGetBool(141, out var value); // ResetSeqNumFlag
            Assert.Multiple(() =>
            {
                Assert.That(found, Is.True);
                Assert.That(value, Is.True);
            });
        }

        [Test]
        public void TryGetBool_Returns_False_When_Missing()
        {
            var view = _views[0];
            var found = view.TryGetBool(9999, out var value);
            Assert.Multiple(() =>
            {
                Assert.That(found, Is.False);
                Assert.That(value, Is.False);
            });
        }

        // ===== Lightweight Repeated Tag Iteration Tests =====

        [Test]
        public void CountTag_Returns_Count_Of_Occurrences()
        {
            var view = _views[0];
            // In a Logon message, most tags appear once
            var count = view.CountTag(35); // MsgType
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void CountTag_Returns_Zero_For_Missing_Tag()
        {
            var view = _views[0];
            var count = view.CountTag(9999);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void ForEachTagPosition_Invokes_Callback_For_Each_Occurrence()
        {
            var view = _views[0];
            var positions = new List<int>();
            var count = view.ForEachTagPosition(35, pos => positions.Add(pos));
            Assert.Multiple(() =>
            {
                Assert.That(count, Is.EqualTo(1));
                Assert.That(positions, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void EnumerateTagPositions_Yields_All_Positions()
        {
            var view = _views[0];
            var positions = new List<int>();
            foreach (var pos in view.EnumerateTagPositions(35))
            {
                positions.Add(pos);
            }
            Assert.That(positions, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetAllTagPositions_Returns_All_Positions()
        {
            var view = _views[0];
            var positions = view.GetAllTagPositions(35);
            Assert.That(positions, Has.Length.EqualTo(1));
        }

        [Test]
        public void GetAllTagPositions_Returns_Empty_For_Missing_Tag()
        {
            var view = _views[0];
            var positions = view.GetAllTagPositions(9999);
            Assert.That(positions, Is.Empty);
        }

        // ===== Position-based access tests =====

        [Test]
        public void GetSpanAtPosition_Returns_Value_At_Position()
        {
            var view = _views[0] as AsciiView;
            Assert.That(view, Is.Not.Null);

            // Get position for tag 35 and access via position
            foreach (var pos in view!.EnumerateTagPositions(35))
            {
                var span = view.GetSpanAtPosition(pos);
                Assert.That(span.Length, Is.EqualTo(1));
                Assert.That(span[0], Is.EqualTo((byte)'A'));
            }
        }

        [Test]
        public void IsEqualAtPosition_Compares_Value_At_Position()
        {
            var view = _views[0] as AsciiView;
            Assert.That(view, Is.Not.Null);

            foreach (var pos in view!.EnumerateTagPositions(35))
            {
                var isEqual = view.IsEqualAtPosition(pos, "A"u8);
                Assert.That(isEqual, Is.True);
            }
        }

        [Test]
        public void TryGetInt32AtPosition_Returns_Value_At_Position()
        {
            var view = _views[0] as AsciiView;
            Assert.That(view, Is.Not.Null);

            foreach (var pos in view!.EnumerateTagPositions(9)) // BodyLength
            {
                var found = view.TryGetInt32AtPosition(pos, out var value);
                Assert.Multiple(() =>
                {
                    Assert.That(found, Is.True);
                    Assert.That(value, Is.EqualTo(208));
                });
            }
        }
    }
}
