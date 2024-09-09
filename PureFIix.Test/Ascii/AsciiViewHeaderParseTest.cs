using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Tag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.ParserFormat;
using PureFix.Types.FIX4._4.quickfix.set;

namespace PureFIix.Test.Ascii
{
    public class AsciiViewHeaderParseTest
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
        public void Get_Header_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var sh = mv.GetView("StandardHeader");
            Assert.That(sh, Is.Not.Null);
        }

        [Test]
        public void Parse_Header_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var sh = mv.GetView("StandardHeader");
            Assert.That(sh, Is.Not.Null);
            var instance = new StandardHeader();
            Console.WriteLine(sh.ToString());
            var sw = new Stopwatch();
            sw.Start();
            const int count = 100000;
            for (var i = 1; i < count; ++i)
            {
                instance.Parse(sh);
            }
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds} {(decimal)sw.ElapsedMilliseconds / count * 1000}  micro/msg");
        }
    }
}
