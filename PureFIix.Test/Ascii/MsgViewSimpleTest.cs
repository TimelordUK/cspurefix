using PureFIix.Test.Env;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PureFIix.Test.Ascii
{
    public class MsgViewSimpleTest
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

        [Test]
        public void Get_Single_View_Test()
        {
            Assert.That(_views, Has.Count.EqualTo(1));
        }

        [Test]
        public void Get_Tag_8_As_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetString(8);
            Assert.That(asString, Is.EqualTo("FIX4.4"));
        }

        [Test]
        public void Get_Tag_9_Typed_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetTyped<string>(9);
            Assert.That(asString, Is.EqualTo("0000208"));
        }

        [Test]
        public void Get_Tag_9_Typed_Int_View_Test()
        {
            var view = _views[0];
            var asInt = view.GetTyped<int>(9);
            Assert.That(asInt, Is.EqualTo(208));
            var asDecimal = view.GetTyped<decimal>(9);
            Assert.That(asDecimal, Is.EqualTo(208d));
        }

        [Test]
        public void Get_Tag_35_As_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetString(35);
            Assert.That(asString, Is.EqualTo("A"));
        }

        [Test]
        public void Get_Tag_56_As_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetString(56);
            Assert.That(asString, Is.EqualTo("target-20"));
        }

        [Test]
        public void Get_Tag_56_Typed_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetTyped<string>(56);
            Assert.That(asString, Is.EqualTo("target-20"));
        }

        [Test]
        public void Get_Tag_96_Typed_Buffer_View_Test()
        {
            var view = _views[0];
            var asBuffer = view.GetTyped<byte[]>(96);
            Assert.That(asBuffer, Is.EqualTo("VgfoSqo56NqSVI1fLdlI"u8.ToArray()));
        }

        [Test]
        public void Get_Tag_141_Typed_Bool_View_Test()
        {
            var view = _views[0];
            var asBool = view.GetTyped<bool>(141);
            Assert.That(asBool, Is.True);
        }

        [Test]
        public void Get_Tag_464_Typed_Bool_View_Test()
        {
            var view = _views[0];
            var asBool = view.GetTyped<bool>(464);
            Assert.That(asBool, Is.False);
        }

        [Test]
        public void Get_Tag_52_Typed_Utc_Timestamp_View_Test()
        {
            var format = ElasticBuffer.TimeFormats.Timestamp;
            DateTime.TryParseExact("20180610-10:39:01.621", format, null, DateTimeStyles.AssumeUniversal, out var d);
            var view = _views[0];
            var asUtc = view.GetTyped<DateTime>(52);
            Assert.That(asUtc, Is.EqualTo(d));
        }

        [Test]
        public void Get_Tag_999_As_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetString(999);
            Assert.That(asString, Is.Null);
        }

        [Test]
        public void Get_All_Strings_View_Test()
        {
            var view = _views[0];
            var asStrings = view.GetStrings();
            Assert.That(asStrings, Has.Length.EqualTo(22));
            Assert.That(asStrings[0], Is.EqualTo("FIX4.4"));
        }
    }
}
