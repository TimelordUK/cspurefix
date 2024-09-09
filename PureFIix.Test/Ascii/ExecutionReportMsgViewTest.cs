using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    internal class ExecutionReportMsgViewTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.ExecutionReportReplayPath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Get_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
        }

        [Test]
        public void Get_RepeatedTag_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var values = mv.GetStrings(803);
            Assert.That(values,Is.EqualTo((string[]) ["22","10","12","13","18","6"]));
        }
    }
}
