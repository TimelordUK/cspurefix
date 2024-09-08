using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    public class MsgViewTest
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
            Assert.That(_views.Count, Is.EqualTo(1));
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
        public void Get_Tag_999_As_String_View_Test()
        {
            var view = _views[0];
            var asString = view.GetString(999);
            Assert.That(asString, Is.Null);
        }
    }
}
